using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using NewProject.responseModel;
using NewProject.ResquestBodyAccount;
using System.Security.Cryptography;
using System.Text;

namespace NewProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbaiContext _context;

        public AdminController(DbaiContext context)
        {
            _context = context;
        }
        [Route("/pagelg")]
        public ActionResult Index()
        {
            return View();
        }
        void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        // GET: AdminController
        private readonly string aesKey = "uWg0B9f5LQvqG59TfZG7NHxLfLj8hMIRxmg0Hzl8UkE="; // Phải khớp với key trong JavaScript

        [HttpPost("/login/page")]
        public async Task<IActionResult> Login([FromBody] ResquestBodyLogin user_pass)
        {
            var res = new Response();

            // Giải mã username và password từ phía client
            string decryptedUsername = DecryptData(user_pass.user, aesKey);
            string decryptedPassword = DecryptData(user_pass.passWord, aesKey);

            if (string.IsNullOrEmpty(decryptedUsername))
            {
                res.Mes = "Failed";
                res.data = "Username is required.";
                return Json(res);
            }

            if (string.IsNullOrEmpty(decryptedPassword))
            {
                res.Mes = "Failed";
                res.data = "Password is required.";
                return Json(res);
            }

            // Search for a matching username and password hash
            var user = await _context.UserLogins
                .Where(u => u.Username == decryptedUsername && u.PasswordHash == decryptedPassword)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                SetSession("Username", user.Username);
                SetSession("UserId", user.UserId.ToString());
                
                res.Mes = "Login successful";
                res.data = "";
                return Json(res);
                 
            }

            res.Mes = "Login failed";
            res.data = "Invalid username or password.";
            return Json(res);
        }

        public static string DecryptData(string encryptedData, string keyBase64)
        {
            // Chuyển đổi khóa từ Base64 thành byte[]
            byte[] keyBytes = Convert.FromBase64String(keyBase64);

            // Tách IV và dữ liệu mã hóa (giả sử định dạng IV:encryptedData)
            string[] parts = encryptedData.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Dữ liệu mã hóa không hợp lệ");
            }

            // Chuyển đổi IV và dữ liệu mã hóa từ Base64
            byte[] iv = Convert.FromBase64String(parts[0]);
            byte[] cipherTextBytes = Convert.FromBase64String(parts[1]);

            // Giải mã dữ liệu AES với khóa và IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = iv;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (var msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Đọc và trả về chuỗi UTF-8 sau khi giải mã
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            // Xóa session khi đăng xuất
            HttpContext.Session.Clear();

            var res = new Response();
            res.Mes = "22";
            res.data = "logout ok";
            return Json(res);
        }


        // GET: AdminController/Details/5

    }
}

using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using NewProject.ResquestBodyAccount;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using System.Security.Cryptography;
using NewProject.responseModel;
namespace NewProject.Controllers
{
    public class UserLoginsController : BaseController
    {
        private readonly DbaiContext _context;

        public UserLoginsController(DbaiContext context)
        {
            _context = context;
        }

        // POST: Login validation
       
       

        // GET: Retrieve all users as JSON
        [HttpGet]
        public async Task<JsonResult> GetAllUsers(string search = "", int page = 1, int pageSize = 10)
        {
            var usersQuery = _context.UserLogins.AsQueryable();

            // Nếu có từ khóa tìm kiếm, thực hiện lọc theo username hoặc email
            if (!string.IsNullOrEmpty(search))
            {
                usersQuery = usersQuery.Where(u => u.Username.Contains(search) || u.Email.Contains(search));
            }

            // Tính tổng số lượng kết quả để xác định còn trang tiếp theo hay không
            int totalUsers = await usersQuery.CountAsync();

            // Lấy danh sách người dùng theo trang hiện tại
            var users = await usersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Kiểm tra xem có trang tiếp theo hay không
            bool hasMore = totalUsers > page * pageSize;

            return Json(new { users, hasMore });
        }

        public IActionResult Index(string searchString, int? page)
        {
            var usersQuery = _context.UserLogins.AsQueryable();

            // Nếu có chuỗi tìm kiếm, thực hiện lọc
            if (!string.IsNullOrEmpty(searchString))
            {
                usersQuery = usersQuery.Where(u => u.Username.Contains(searchString) || u.Email.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Lấy danh sách người dùng theo trang
            var users = usersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Tạo phân trang
            var pagedList = new StaticPagedList<UserLogin>(users, pageNumber, pageSize, usersQuery.Count());

            return View(pagedList);
        }

        // POST: Create a new user
        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody] ResquestCreatUser model)
        {
            
             var map=   new UserLogin
                {
                      // Thông thường bạn có thể bỏ qua vì nó sẽ được tự động tăng
                    Username = model.Username,
                    PasswordHash = model.PasswordHash,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,

                    // Các giá trị mặc định hoặc null khi tạo mới người dùng
                    CreatedDate = DateTime.UtcNow,  // Ngày tạo tài khoản
                    LastLoginDate = null,           // Chưa đăng nhập
                    IsActive = true,                // Người dùng có thể hoạt động ngay từ đầu
                    FailedLoginAttempts = 0,        // Số lần đăng nhập thất bại ban đầu
                    LockedOutUntil = null           // Chưa bị khóa
                };
                _context.UserLogins.Add(map);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            
 
        }

        // POST: Edit an existing user
        [HttpPost]
        public async Task<JsonResult> EditUser([FromBody] UserLogin model)
        {
            var user = await _context.UserLogins.FirstOrDefaultAsync(u => u.UserId == model.UserId);

            if (user != null)
            {
                user.Username = model.Username;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.IsActive = model.IsActive;

                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        // POST: Delete an existing user
        [HttpPost]
        public async Task<JsonResult> DeleteUser(int id)
        {
            var user = await _context.UserLogins.FirstOrDefaultAsync(u => u.UserId == id);

            if (user != null)
            {
                _context.UserLogins.Remove(user);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}

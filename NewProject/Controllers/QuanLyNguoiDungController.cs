using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using NewProject.ResModel;
using Newtonsoft.Json;
using System.Diagnostics;

namespace NewProject.Controllers
{
    public class QuanLyNguoiDungController : BaseController
    {
 //        <th class="border-bottom border-top">Id</th>
 //<th class="border-bottom border-top">Họ và tên</th>
 //<th class="border-bottom border-top">Ngày tháng năm sinh</th>
 //<th class="border-bottom border-top">Mã số sinh viên</th>
 //<th class="border-bottom border-top">Giới tính</th>
 //<th class="border-bottom border-top">Chuyên ngành</th>
 //<th class="border-bottom border-top">Khoa</th>
 //<th class="border-bottom border-top">Ảnh</th>
 //<th class="border-bottom border-top">Thời gian đăng ký</th>
                     
                  
 //<th class="border-bottom border-top">Actions</th>
        private static readonly List<object> students = new List<object>
        {
            new { Id = 1, Name = "John Doe", NgayThangNamSinh = 22,MaSoSinhVien = "23140016",GioiTinh = "Nam",
                ChuyenNganh = "Ky Thuat Phan Mem",Khoa = "Cong Nghe Thong Tin",Anh = "syanh.jpg",ThoiGianDangKy = "09/09/2024",
                Action = "Action"  },
          new { Id = 2, Name = "John Doe 2", NgayThangNamSinh = 22,MaSoSinhVien = "23140017",GioiTinh = "Nam",
    ChuyenNganh = "Ky Thuat Phan Mem",Khoa = "Cong Nghe Thong Tin",Anh = "syanh.jpg",ThoiGianDangKy = "09/09/2000",Action = "Action"  },
          new { Id = 3, Name = "John Doe 3", NgayThangNamSinh = 22,MaSoSinhVien = "23140018",GioiTinh = "Nam",
    ChuyenNganh = "Ky Thuat Phan Mem",Khoa = "Cong Nghe Thong Tin",Anh = "syanh.jpg",ThoiGianDangKy = "09/09/2023",Action = "Action"  },
          new { Id = 4, Name = "John Doe 4", NgayThangNamSinh = 22,MaSoSinhVien = "23140019",GioiTinh = "Nam",
    ChuyenNganh = "Ky Thuat Phan Mem",Khoa = "Cong Nghe Thong Tin",Anh = "syanh.jpg",ThoiGianDangKy = "09/09/2022",Action = "Action"  }
        };
        private readonly ILogger<QuanLyNguoiDungController> _logger;
        
        public QuanLyNguoiDungController(ILogger<QuanLyNguoiDungController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
               
        
       
        [HttpGet("/getalllist")]
        //[Route("/getalllist")]
        public async Task< ActionResult<IEnumerable<object>>> GetAllList()
        {
            Service.HttpClient_API get = new Service.HttpClient_API();
            string url = "http://127.0.0.1:8000";
            string EP = "/students";
          string res= await get.GET(url,EP);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(res);

            return Json(students);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

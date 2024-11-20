using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using System.Diagnostics;

namespace NewProject.Controllers
{
    public class DangKyThongTinController : BaseController
    {

        private readonly ILogger<DangKyThongTinController> _logger;
        
        public DangKyThongTinController(ILogger<DangKyThongTinController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

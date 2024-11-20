using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NewProject.Controllers
{
    public class BaseController : Controller
    {
        // Hàm tiện ích để lưu dữ liệu vào Session
        protected void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        // Hàm tiện ích để lấy dữ liệu từ Session
        protected string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        // Kiểm tra nếu người dùng chưa đăng nhập, chuyển hướng về trang Login
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = GetSession("Username");

            // Nếu session không tồn tại, người dùng chưa đăng nhập
            if (string.IsNullOrEmpty(username))
            {
               
               
               context.Result = new RedirectResult("/pagelg");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

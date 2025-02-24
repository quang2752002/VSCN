using GUIs.Helper;
using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;

namespace VSCN.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult DangNhap(string username, string password)
        {
            try
            {
                AdminDAO adminDAO = new AdminDAO();
                string matkhau = Support.HashPassword(password);
                int x = adminDAO.Login(username, matkhau);
                string router = "";
                string mess = "Đăng nhập không thành công";
                bool status = false;
                if (x != -1)
                {
                    HttpContext.Session.SetInt32(CustomeCommon.USER_ID, x);
                    router = "Admin";
                    HttpContext.Session.SetString(CustomeCommon.ROUTER, router);
                    status = true;
                }
                return Json(new { mess = mess, status = status, router = router });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        [HttpGet]
        public JsonResult CheckLogin()
        {
            try
            {
                AdminDAO adminDAO = new AdminDAO();
                bool status = false;
                int id = HttpContext.Session.GetInt32(CustomeCommon.USER_ID) ?? 0;
                if (id != 0)
                    return Json(new { status = true });
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
    }
}

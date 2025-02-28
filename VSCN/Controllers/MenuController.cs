using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;
using VSCN.Models.VIEW;

namespace VSCN.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ShowList()
        {
            try
            {

                MenuDAO menuDAO = new MenuDAO();
                (int total, List<MenuVIEW> query) = menuDAO.ShowList();
                return Json(new
                {
                    success = true,
                    data = query,
                });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
    }
}

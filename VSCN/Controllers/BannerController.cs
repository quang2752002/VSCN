using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;
using VSCN.Models.VIEW;

namespace VSCN.Controllers
{
    public class BannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ShowList()
        {
            try
            {

                BannerDAO bannerDAO = new BannerDAO();
                (int total, List<BannerVIEW> query) = bannerDAO.ShowList();
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

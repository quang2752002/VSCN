using Microsoft.AspNetCore.Mvc;
using VSCN.Helper;
using VSCN.Models.DAO;
using VSCN.Models.VIEW;

namespace VSCN.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ShowList()
        {
            try
            {
               ProductDAO productDAO = new ProductDAO();
                (int total,List<ProductVIEW> listProduct) = productDAO.ShowList();
                return Json(new
                {
                    data =listProduct,
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VSCN.Helper;
using VSCN.Models.DAO;
using VSCN.Models.VIEW;

namespace VSCN.Controllers
{
    public class ProductController : Controller
    {
        private const string PRODUCT_ID = "PRODUCT_ID";
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
            HttpContext.Session.SetString(PRODUCT_ID, id);
            ProductDAO productDAO = new ProductDAO();
            ViewBag.ListProduct = productDAO.GetList(Common.SERVICE);

            return View();
        }
        public JsonResult getProduct( )
        {
            try
            {
                string id = HttpContext.Session.GetString(PRODUCT_ID) ?? "";

                ProductDAO productDAO = new ProductDAO();
                var query = productDAO.GetItemBySlug(id);
                return Json(new
                {
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

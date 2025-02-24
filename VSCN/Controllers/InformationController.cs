using Microsoft.AspNetCore.Mvc;
using VSCN.Helper;
using VSCN.Models.DAO;

namespace VSCN.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult getInfor()
        {
            try
            {
                InformationDAO informationDAO = new InformationDAO();
                string phone = informationDAO.getItem(Common.PHONE);
                string email = informationDAO.getItem(Common.EMAIL);
                string intro = informationDAO.getItem(Common.INTRO);
                string address = informationDAO.getItem(Common.ADDRESS);
                return Json(new
                {
                    Phone = phone,
                    Email = email,
                    Intro = intro,
                    Address = address
                });
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
            
        }

    }
}

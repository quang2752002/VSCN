using Microsoft.AspNetCore.Mvc;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

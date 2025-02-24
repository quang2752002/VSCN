using Microsoft.AspNetCore.Mvc;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace VSCN.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

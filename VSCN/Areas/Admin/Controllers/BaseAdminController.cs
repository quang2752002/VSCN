using GUIs.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VSCN.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.AreaName = "/User";
            int userid = DataServices.getUserId(HttpContext);
            if (userid <= 0)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login", area = "" }));
            }
            else
            {
                string route = DataServices.getRouoter(HttpContext);

                //if (route == "User")
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home", area = "" }));
                //}
                if (route == "Admin")
                {
                    // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Admin", area = route }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

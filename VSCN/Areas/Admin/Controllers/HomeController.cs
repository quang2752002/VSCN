﻿using Microsoft.AspNetCore.Mvc;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

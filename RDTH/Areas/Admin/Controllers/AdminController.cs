using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        [Route("Admin")]
        [Route("Admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
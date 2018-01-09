using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
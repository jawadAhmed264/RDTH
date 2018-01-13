using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;
using RDTH.Models.ModViewModel;

namespace RDTH.Controllers
{
    [Authorize(Roles = "User")]
    public class ModController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IMod _modservice;
        private readonly ICustomer _cusService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ModController(IStatusService statusService,
            IMod modService,
            ICustomer CusService,
            UserManager<ApplicationUser> userManager)
        {
            _statusService = statusService;
            _modservice = modService;
            _userManager = userManager;
            _cusService = CusService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ModAddModel model)
        {
            if (ModelState.IsValid)
            {
                MovieOnDemand movie = new MovieOnDemand
                {
                    Movie = model.Movie,
                    MovieTime = model.MovieTime,
                    Status = _statusService.GetByName("Pending"),
                    Customer = _cusService.GetByUser(_userManager.GetUserId(HttpContext.User))
                };
                _modservice.Add(movie);

                ViewBag.success = "success";
                ViewBag.msg = "Your Request Has been send to Admin, Thank You";

                ModelState.Clear();
                return View();
            }
            return View(model);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models;

namespace RDTH.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDealerService _dealerService;
        private readonly IDistributerService _disService;
        UserManager<ApplicationUser> _userManager;

        public OrderController(
            IOrder orderService,
            IDealerService dealerService,
            IDistributerService disService,
            UserManager<ApplicationUser> userManager)
        {
            _dealerService = dealerService;
            _disService = disService;
            _userManager = userManager;

        }

        [Authorize(Roles = "Dealer")]
        public IActionResult DealerOrder()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var dealer = _dealerService.GetByUserId(userId);
            var model = _dealerService.GetOrders(dealer.Id);
            return View(model);
        }

        [Authorize(Roles = "Distributer")]
        public IActionResult DistributerOrder()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var distributer = _disService.GetByUserId(userId);
            var model = _disService.GetOrders(distributer.Id);
            return View(model);
        }
    }
}
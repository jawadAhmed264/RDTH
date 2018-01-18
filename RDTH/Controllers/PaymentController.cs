using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models;

namespace RDTH.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrder _orderService;
        private readonly IDealerService _dealerService;
        private readonly IDistributerService _disService;
        UserManager<ApplicationUser> _userManager;

        public PaymentController(
            IOrder orderService,
            IDealerService dealerService,
            IDistributerService disService,
            UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _dealerService = dealerService;
            _disService = disService;
            _userManager = userManager;

        }

        [Authorize(Roles = "Dealer")]
        public IActionResult DealerPayment()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var dealer = _dealerService.GetByUserId(userId);
            var model = _dealerService.GetPayments(dealer.Id);
            return View(model);
        }

        [Authorize(Roles = "Distributer")]
        public IActionResult DistributerPayment()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var distributer = _disService.GetByUserId(userId);
            var model = _disService.GetPayments(distributer.Id);
            return View(model);
        }
    }
}
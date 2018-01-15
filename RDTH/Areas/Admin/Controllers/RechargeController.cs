using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RechargeController : Controller
    {
        private readonly IRechargeService _rechargeService;

        public RechargeController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }
        public IActionResult Index()
        {
            var rechargeHistory = _rechargeService.GetAll();
            return View(rechargeHistory);
        }
    }
}
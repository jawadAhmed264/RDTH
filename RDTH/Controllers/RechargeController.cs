using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;
using RDTH.Models.RechargeViewModel;
using System;
using System.Linq;

namespace RDTH.Controllers
{
    public class RechargeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRechargeService _rechargeService;
        private readonly ICardService _cardService;
        private readonly IPackageService _packageService;
        private readonly ICustomerPackage _cpService;
        private readonly IStatusService _statusService;

        public RechargeController(
            IRechargeService rechargeService,
            IPackageService packageService,
            ICardService cardService,
            ICustomerPackage cpService,
            IStatusService statusService,
            UserManager<ApplicationUser> UserManager)
        {
            _userManager = UserManager;
            _rechargeService = rechargeService;
            _cardService = cardService;
            _packageService = packageService;
            _cpService = cpService;
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RechargeAddModel model)
        {
            DateTime now = DateTime.Now;

            if (model.CardExpiry < now)
            {
                ModelState.AddModelError("CardExpiry", "Your Credit Card is Expire");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var card = GetCard(model.Customercard);
                var package = GetPackage(card.Package.Id);

                Recharge recharge = new Recharge()
                {
                    CardExpiry = model.CardExpiry,
                    Cost = model.Total,
                    CreditCardNumber = model.CreditCardNumber,
                    CVV = model.CVV,
                    PaymentType = model.PaymentType,
                    RechargeDate = now,
                    CustomerCard = card,
                    Package = package
                };

                var cp = _cpService.GetByCardId(card.Id);
                cp.Status = _statusService.GetByName("Charged");
                cp.ExpirationDate = now.AddMonths(model.Months);
                cp.NumberOfMonths = model.Months;
                cp.Package = card.Package;
                cp.CustomerCard = card;

                _cpService.Update(cp);
                _rechargeService.Add(recharge);

                ModelState.Clear();
                ViewBag.success = "success";
                ViewBag.msg = "Your Account has been Recharge ,Thank You! for chossing our service";
                return View();

            }

            return View(model);
        }

        public IActionResult History()
        {
            CustomerCard card = _cardService.GetCurrentUserCard(_userManager.GetUserId(HttpContext.User));


            var rechargeHistory = _rechargeService.GetByCustomerCard(card.CardNumber).
                Select(r=>new RechargeHistoryDetailModel {
                    Charges=r.Package.Charges,
                    Customercard=r.CustomerCard.CardNumber,
                    Total=r.Cost,
                    OwnerName=r.CustomerCard.OwnerName,
                    PackageName=r.Package.PackageName,
                    PaymentType=r.PaymentType,
                    RechargeDate=r.RechargeDate
                });

            RechargeHistoryIndexModel model = new RechargeHistoryIndexModel {
                RechargeList = rechargeHistory
            };
            return View(model);
        }


        public JsonResult IsCardAvailable(string Customercard)
        {
            return Json(_cardService.CheckCard(Customercard));
        }

        public JsonResult GetCost(string Customercard)
        {
            var card = _cardService.GetCardByNumber(Customercard);
            decimal? charges = _packageService.GetPackageCharges(card.Package.Id);
            return Json(charges);
        }

        private CustomerCard GetCard(string c) {
            return _cardService.GetCardByNumber(c);
        }

        private Package GetPackage(int PackageId)
        {
            return _packageService.GetById(PackageId);
        }
    }
}
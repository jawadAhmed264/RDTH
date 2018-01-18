using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;
using RDTH.Models.PackageViewModel;
using RDTH.Models.SetBoxViewModel;
using System;
using System.Linq;

namespace RDTH.Controllers
{
    public class PackageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPackageService _packageService;
        private readonly ISetBoxService _sbService;
        private readonly ICardService _cardService;
        private readonly ICustomerPackage _cpService;
        private readonly ICustomer _cusService;
        private readonly IStatusService _statusService;

        public PackageController(IPackageService packageService,
            ICustomerPackage cpService,
            ISetBoxService sbService,
            ICardService CardService,
            ICustomer cusService,
            IStatusService statusService,
            UserManager<ApplicationUser> UserManager)
        {
            _packageService = packageService;
            _userManager = UserManager;
            _cardService = CardService;
            _sbService = sbService;
            _cpService = cpService;
            _cusService = cusService;
            _statusService = statusService;
        }

        public IActionResult Index()
        {
            var packages = _packageService.GetAll().Select(p => new PackageDetailViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Charges = p.Charges,
                PackageName = p.PackageName,
                DocumentariesChannel = p.DocumentariesChannel,
                EntertainmentChannel = p.EntertainmentChannel,
                NewsChannel = p.NewsChannel,
                NoOfChannels = p.NoOfChannels,
                SportsChannel = p.SportsChannel

            });
            var model = new PackageIndexViewModel() { PackagesDetailList = packages };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var package = _packageService.GetById(id);
            var model = new PackageDetailViewModel()
            {
                Id = id,
                ImageUrl = package.ImageUrl,
                Charges = package.Charges,
                PackageName = package.PackageName,
                DocumentariesChannel = package.DocumentariesChannel,
                EntertainmentChannel = package.EntertainmentChannel,
                NewsChannel = package.NewsChannel,
                NoOfChannels = package.NoOfChannels,
                SportsChannel = package.SportsChannel

            };

            return View(model);
        }

        [Authorize(Roles = "User")]
        public IActionResult MyPackage()
        {
            CustomerCard card = _cardService.GetCurrentUserCard(_userManager.GetUserId(HttpContext.User));
            Package package = _packageService.GetById(card.Package.Id);
            SetBox SetBox = _sbService.GetById(card.SetBox.Id);
            Customer cus = _cusService.GetByUser(_userManager.GetUserId(HttpContext.User));
            DateTime expire = _cpService.GetExpirationTime(cus.Id);
            CustomerPackage cp = _cpService.GetByCardId(card.Id);

            string status = UpdatePakageStatus(cp, expire);

            MyPackageViewModel model = new MyPackageViewModel
            {
                MyPackage = new PackageDetailViewModel
                {
                    PackageName = package.PackageName,
                    NoOfChannels = package.NoOfChannels,
                    Charges = package.Charges,
                    ImageUrl = package.ImageUrl
                },
                MySetBox = new SetBoxDetailModel
                {
                    Name = SetBox.Name,
                    Specification = SetBox.Specification,
                    Price = SetBox.Price,
                    ImageUrl = SetBox.ImageUrl
                },
                GetExpiration = expire,
                State = status
            };
            return View(model);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult ChangePackage(int? PackageId)
        {
            if (PackageId == null)
            {
                return NotFound();
            }
            var package = _packageService.GetById(PackageId);

            if (package == null)
            {
                return NotFound();
            }

            var model = new PackageDetailViewModel
            {
                Id = package.Id,
                PackageName = package.PackageName
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult ChangePackage(PackageDetailViewModel model)
        {

            if (ModelState.IsValid)
            {
                var package = _packageService.GetById(model.Id);

                if (package == null)
                {
                    return NotFound();
                }
                var card = _cardService.GetCurrentUserCard(_userManager.GetUserId(HttpContext.User));
                if (card == null)
                {
                    return NotFound();
                }
                card.Package = package;
                _cardService.Update(card);

                var cp = _cpService.GetByCardId(card.Id);
                cp.Package = package;
                cp.Status = _statusService.GetByName("Recharged");
                _cpService.Update(cp);

                ViewBag.success = "success";
                ViewBag.msg = "Your Package Has been Changed. You need to Recharged Your Package.";
                return RedirectToAction("MyPackage");
            }

            return View(model);
        }

        private string UpdatePakageStatus(CustomerPackage cp, DateTime expire)
        {
            if (expire < DateTime.Now)
            {
                cp.Status = _statusService.GetByName("Recharged");
                _cpService.Update(cp);
                return cp.Status.Name;
            }
            else
            {
                return cp.Status.Name;
            }
        }


    }
}
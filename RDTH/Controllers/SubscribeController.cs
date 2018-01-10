using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models.SubscribeViewModel;
using System;
using System.Linq;

namespace RDTH.Controllers
{
    public class SubscribeController : Controller
    {
        private ISubscribeService _subService;
        private IPackageService _packageService;
        private ISetBoxService _setBoxService;
        private IStatusService _statusService;

        public SubscribeController(ISubscribeService subService, IPackageService pakService, ISetBoxService sbService,IStatusService statusService)
        {
            _subService = subService;
            _packageService = pakService;
            _setBoxService = sbService;
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetDropDown();

            return View();
        }

        [HttpPost]
        public IActionResult Index(SubscribeAddModel newSub)
        {
            SetDropDown();

            if (ModelState.IsValid)
            {
                var model = newSub;
                _subService.AddSubscribe(new NewSubscribe()
                {
                    OwnerName = model.OwnerName,
                    Address = model.Address,
                    ApplyDate = DateTime.Now,
                    ContactNumber = model.ContactNumber,
                    Package = _packageService.GetById(model.PackageId),
                    SetBox = _setBoxService.GetById(model.SetBoxId),
                    Status = _statusService.GetByName("Pending")
                });
                ModelState.Clear();
                ViewBag.success = "success";
                ViewBag.msg = "Request Send Successfully, We will contact you soon";
                return View();
            }
            return View(newSub);
        }

        private void SetDropDown() {
            ViewBag.Packages = _packageService.GetAll().ToList();
            ViewBag.SetBoxes = _setBoxService.GetAll().ToList();
        }

    }
}
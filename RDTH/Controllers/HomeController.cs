using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models;
using RDTH.Models.HomeViewModel;

namespace RDTH.Controllers
{
    public class HomeController : Controller
    {
        private ISetBoxService _setBoxService;
        private IPackageService _packageService;

        public HomeController(ISetBoxService setBoxService, IPackageService packageService)
        {
            _setBoxService = setBoxService;
            _packageService = packageService;
        }
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                Packages = _packageService.GetLatest(),
                SetBoxes = _setBoxService.GetLatest()

            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

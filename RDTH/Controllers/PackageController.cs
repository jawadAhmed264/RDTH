using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models.PackageViewModel;
using System.Linq;

namespace RDTH.Controllers
{
    public class PackageController : Controller
    {
        private IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public IActionResult Index()
        {
            var packages = _packageService.GetAll().Select(p => new PackageDetailViewModel()
            {
                Id=p.Id,
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
                Id=id,
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
    }
}
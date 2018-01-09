using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models.SetBoxViewModel;

namespace RDTH.Controllers
{
    public class SetboxController : Controller
    {
        private ISetBoxService _SetBoxService;

        public SetboxController(ISetBoxService setboxService)
        {
            _SetBoxService = setboxService;
        }

        public IActionResult Index()
        {
            var devices = _SetBoxService.GetAll().Select(s => new SetBoxDetailModel()
            {
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Name = s.Name,
                Price = s.Price,
                Specification = s.Specification
            });
            var model = new SetBoxIndexModel() { SetBoxList = devices };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var setBox = _SetBoxService.GetById(id);
            var model = new SetBoxDetailModel()
            {
                Id = setBox.Id,
                ImageUrl = setBox.ImageUrl,
                Name = setBox.Name,
                Price = setBox.Price,
                Specification = setBox.Specification
            };

            return View(model);
        }
    }
}
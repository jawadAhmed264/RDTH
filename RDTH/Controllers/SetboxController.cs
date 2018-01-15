using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;
using RDTH.Models.SetBoxViewModel;

namespace RDTH.Controllers
{
    public class SetboxController : Controller
    {
        private readonly ISetBoxService _SetBoxService;
        private readonly INewSetBoxRequest _setBoxRequest;
        private readonly ICardService _cardService;
        private readonly IStatusService _statusService;
        private readonly UserManager<ApplicationUser> _userManager;


        public SetboxController(
            ISetBoxService setboxService,
            INewSetBoxRequest setBoxRequest,
            ICardService cardService,
            IStatusService statusService,
            UserManager<ApplicationUser> userManager
            )
        {
            _SetBoxService = setboxService;
            _setBoxRequest = setBoxRequest;
            _statusService = statusService;
            _cardService = cardService;
            _userManager = userManager;
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
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult ChangeSetBox(int? setBoxId)
        {
            if (setBoxId == null)
            {
                return NotFound();
            }
            var setBox = _SetBoxService.GetById(setBoxId);

            if (setBox == null)
            {
                return NotFound();
            }

            var model = new SetBoxDetailModel
            {
                Id = setBox.Id,
                Name = setBox.Name
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ChangeSetBox(SetBoxDetailModel model)
        {
            var card = _cardService.GetCurrentUserCard(_userManager.GetUserId(HttpContext.User));

            if (card == null)
            {
                return NotFound();
            }
            if (_setBoxRequest.CheckAlreadyApplied(card.CardNumber))
            {
                return View("AlreadyApplied");
            }

            if (ModelState.IsValid)
            {
                var request = new NewSetBoxRequest()
                {
                    Setbox = _SetBoxService.GetById(model.Id),
                    Card = card,
                    Status = _statusService.GetByName("Pending")
                };

                _setBoxRequest.Add(request);
                ModelState.Clear();
                ViewBag.success = "success";
                ViewBag.msg = "Your Request sent to admin for approval";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Distributer,Dealer")]
        public IActionResult CustomerSetBoxRequest()
        {
            Status st = _statusService.GetByName("AdminApproved");
            var model = _setBoxRequest.GetByStatus(st.Name);
            return View(model);
        }

        [Authorize(Roles = "Distributer,Dealer")]
        [HttpPost]
        public IActionResult Delivered(NewSetBoxRequest model)
        {
            var request = model;
            request.Status = _statusService.GetByName("Delivered");
            _setBoxRequest.Update(request);
            return RedirectToAction("CustomerSetBoxRequest");
        }
    }
}
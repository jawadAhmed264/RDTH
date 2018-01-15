using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NewSetboxRequestController : Controller
    {
        private readonly INewSetBoxRequest _newSetBoxService;
        private readonly IStatusService _statusService;
        private readonly ISetBoxService _setboxService;
        private readonly ICardService _cardService;


        public NewSetboxRequestController
            (
              INewSetBoxRequest newSetBoxService,
              IStatusService statusService,
              ICardService cardService,
              ISetBoxService setboxService

            )
        {
            _newSetBoxService = newSetBoxService;
            _statusService = statusService;
            _cardService = cardService;
            _setboxService = setboxService;
        }

        public IActionResult Index()
        {
            var model = _newSetBoxService.GetAll();
            return View(model);
        }

        public IActionResult Approved(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var request = _newSetBoxService.GetById(Id);

            if (request == null)
            {
                return NotFound();
            }

            CustomerCard card = _cardService.GetCardByNumber(request.Card.CardNumber);

            card.SetBox = _setboxService.GetById(request.Card.Id);
            _cardService.Update(card);

            request.Status = _statusService.GetByName("AdminApproved");
            _newSetBoxService.Update(request);

            return RedirectToAction("Index");
        }

        // GET: Admin/FeedBack/Delete/5
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var request = _newSetBoxService.GetById(Id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Admin/FeedBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            var request = _newSetBoxService.GetById(Id);
            _newSetBoxService.Remove(request);
            return RedirectToAction(nameof(Index));
        }


    }
}
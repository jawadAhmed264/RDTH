using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models.FeedbackViewModel;
using System;

namespace RDTH.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedback _feedbackService;
        private IStatusService _statusService;
        public FeedbackController(IFeedback feedbackService, IStatusService StatusService)
        {
            _feedbackService = feedbackService;
            _statusService = StatusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FeedbackAddModel model)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.Add(new FeedBack()
                {
                    Date = DateTime.Now,
                    Email = model.Email,
                    Name = model.Name,
                    Msg = model.Msg,
                    Status = _statusService.GetByName("Pending")
                });

                ViewBag.success = "success";
                ViewBag.msg = "Thank You for Your Valuable Feedback.";

                ModelState.Clear();
                return View();
            }

            return View(model);
        }
    }
}
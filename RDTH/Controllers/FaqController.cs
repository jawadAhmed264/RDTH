using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models.FaqViewModel;
using System.Linq;

namespace RDTH.Controllers
{
    public class FaqController : Controller
    {
        private IFaq _faqService;
        public FaqController(IFaq faqService)
        {
            _faqService = faqService;
        }

        public IActionResult Index()
        {
            var faq = _faqService.GetAll().Select(f => new FaqDetailModel
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            });

            FaqIndexModel model = new FaqIndexModel()
            {
               Faqs=faq
            };

            return View(model);
        }
    }
}
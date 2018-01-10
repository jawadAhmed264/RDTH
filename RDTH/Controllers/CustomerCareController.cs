using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models.CustomerCareViewModel;
using System.Linq;

namespace RDTH.Controllers
{
    public class CustomerCareController : Controller
    {
        private IDealerService _dealerService;

        public CustomerCareController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [Route("CustomerCare")]
        public IActionResult GetDealer()
        {
            var dealersList = _dealerService.GetAll().Select(d => new CustomerCareDetailModel()
            {
                Id = d.Id,
                Address = d.Address,
                City = d.City,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Telephone = d.Telephone
            });

            CustomerCareIndexModel model = new CustomerCareIndexModel()
            {
                DealersList = dealersList
            };

            return View(model);
        }
    }
}
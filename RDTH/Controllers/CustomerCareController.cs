using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDTH.Data;
using RDTH.Models.CustomerCareViewModel;
using System.Linq;

namespace RDTH.Controllers
{
    public class CustomerCareController : Controller
    {
        private IDealerService _dealerService;
        private IDistributerService _disService;
        public CustomerCareController(IDealerService dealerService,IDistributerService disService)
        {
            _dealerService = dealerService;
            _disService = disService;
        }

        [Route("CustomerCare")]
        [Authorize(Roles = "User")]
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
        [Route("[Controller]/Distributers")]
        [Authorize(Roles = "Dealer")]
        public IActionResult GetDistributer()
        {
            var dealersList = _disService.GetAll().Select(d => new CustomerCareDetailModel()
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
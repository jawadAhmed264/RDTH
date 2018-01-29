using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;
using RDTH.Models.CheckoutViewModel;
using System;
using System.Threading.Tasks;

namespace RDTH.Controllers
{
    [Authorize(Roles = "Dealer,Distributer")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDealerService _dealerService;
        private readonly IDistributerService _disService;


        public CheckoutController(UserManager<ApplicationUser> userManager,
            IDealerService dealerService,
            IDistributerService distributerService)
        {
            _userManager = userManager;
            _dealerService = dealerService;
            _disService = distributerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string cartSession = HttpContext.Session.GetString("Cart");

            if (cartSession == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));

            CheckoutAddModel model;
            if (await _userManager.IsInRoleAsync(user, "Dealer"))
            {
                var dealer = _dealerService.GetByUserId(user.Id);
                model = new CheckoutAddModel
                {
                    PersonName = dealer.FirstName + " " + dealer.LastName,
                    Contact = dealer.Telephone,
                    ShippingAddress = dealer.Address + "," + dealer.City,
                    Cart = cart
                };
                return View(model);
            }
            if (await _userManager.IsInRoleAsync(user, "Distributer"))
            {
                var distributer = _disService.GetByUserId(user.Id);
                model = new CheckoutAddModel
                {
                    PersonName = distributer.FirstName + " " + distributer.LastName,
                    Contact = distributer.Telephone,
                    ShippingAddress = distributer.Address + "," + distributer.City,
                    Cart = cart
                };
                return View(model);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutAddModel model)
        {
            DateTime now = DateTime.Now;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string cartSession = HttpContext.Session.GetString("Cart");

            if (cartSession == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
            model.Cart = cart;

            if (model.PaymentType == "Cash On Delivery")
            {
                if (await _userManager.IsInRoleAsync(user, "Dealer"))
                {
                    var dealer = _dealerService.GetByUserId(user.Id);

                    var order = new Order
                    {
                        PersonName = model.PersonName,
                        Contact = model.Contact,
                        ShippingAddress = model.ShippingAddress,
                        DatePlaced = now,
                    };

                    var payment = new Payment()
                    {
                        PersonName = model.PersonName,
                        Contact = model.Contact,
                        CreditCardNumber = model.CreditCardNumber,
                        CVV = model.CVV,
                        CardExpiry = model.CardExpiry,
                        PaymentDate = now,
                        Cost = cart.TotalPrice,
                        PaymentType = model.PaymentType,
                    };
                    int result = await _dealerService.PlacedOrderAsync(dealer, cart, order, payment);
                }

                if (await _userManager.IsInRoleAsync(user, "Distributer"))
                {
                    var dis = _disService.GetByUserId(user.Id);
                    var order = new Order
                    {
                        PersonName = model.PersonName,
                        Contact = model.Contact,
                        ShippingAddress = model.ShippingAddress,
                        DatePlaced = now,
                    };

                    var payment = new Payment()
                    {
                        PersonName = model.PersonName,
                        Contact = model.Contact,
                        CreditCardNumber = model.CreditCardNumber,
                        CVV = model.CVV,
                        CardExpiry = model.CardExpiry,
                        PaymentDate = now,
                        Cost = cart.TotalPrice,
                        PaymentType = model.PaymentType,
                    };
                    int result = await _disService.PlacedOrderAsync(dis, cart, order, payment);
                }
                ModelState.Clear();
                TempData["OrderPlaced"] = "success";
                TempData["OrderMsg"] = "Your Order Has been Placed Thank You";
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("Index", "Setbox");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (await _userManager.IsInRoleAsync(user, "Dealer"))
                    {
                        var dealer = _dealerService.GetByUserId(user.Id);

                        var order = new Order
                        {
                            PersonName = model.PersonName,
                            Contact = model.Contact,
                            ShippingAddress = model.ShippingAddress,
                            DatePlaced = now,
                        };

                        var payment = new Payment()
                        {
                            PersonName = model.PersonName,
                            Contact = model.Contact,
                            CreditCardNumber = model.CreditCardNumber,
                            CVV = model.CVV,
                            CardExpiry = model.CardExpiry,
                            PaymentDate = now,
                            Cost = cart.TotalPrice,
                            PaymentType = model.PaymentType,
                        };
                        int result = await _dealerService.PlacedOrderAsync(dealer, cart, order, payment);
                    }

                    if (await _userManager.IsInRoleAsync(user, "Distributer"))
                    {
                        var dis = _disService.GetByUserId(user.Id);
                        var order = new Order
                        {
                            PersonName = model.PersonName,
                            Contact = model.Contact,
                            ShippingAddress = model.ShippingAddress,
                            DatePlaced = now,
                        };

                        var payment = new Payment()
                        {
                            PersonName = model.PersonName,
                            Contact = model.Contact,
                            CreditCardNumber = model.CreditCardNumber,
                            CVV = model.CVV,
                            CardExpiry = model.CardExpiry,
                            PaymentDate = now,
                            Cost = cart.TotalPrice,
                            PaymentType = model.PaymentType,
                        };

                        int result = await _disService.PlacedOrderAsync(dis, cart, order, payment);

                    }

                    ModelState.Clear();
                    TempData["OrderPlaced"] = "success";
                    TempData["OrderMsg"] = "Your Order Has been Placed Thank You";
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Index", "Setbox");
                }
            }
            return View(model);
        }
    }
}
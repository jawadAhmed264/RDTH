using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Controllers
{
    [Authorize(Roles = "Dealer,Distributer")]
    public class CartController : Controller
    {
        private readonly ISetBoxService _setBoxService;
        private static List<CartItem> CartItems = new List<CartItem>();
        private static int CartItemId = 0;

        public CartController
            (
            ISetBoxService setBoxService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _setBoxService = setBoxService;
        }

        public IActionResult Index()
        {
            var CartSession = HttpContext.Session.GetString("Cart");
            if (CartSession != null)
            {
                Cart Cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
                return View(Cart);
            }
            return RedirectToAction("Index", "Setbox");
        }

        //Add Item to Cart
        [HttpPost]
        public IActionResult AddToCart(int Id, int qty)
        {
            var CartSession = HttpContext.Session.GetString("Cart");
            Cart Cart;
            if (CartSession == null)
            {
                Cart = new Cart();
            }
            else
            {
                Cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
            }

            SetBox sb = _setBoxService.GetById(Id);

            CartItem item = new CartItem
            {
                Id = ++CartItemId,
                Product = sb,
                Qty = qty,
                Price = qty * sb.Price
            };
            CartItems.Add(item);
            Cart.ItemList = CartItems;
            Cart.TotalPrice = Cart.ItemList.Sum(m => m.Price);
            Cart.TotalItems = Cart.ItemList.Sum(m => m.Qty);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(Cart));
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var CartSession = HttpContext.Session.GetString("Cart");

            if (CartSession == null)
            {
                return NotFound();
            }

            Cart Cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));

            CartItem item = Cart.ItemList.SingleOrDefault(m => m.Id == Id);

            if (item == null)
            {
                return NotFound();
            }

            CartItems.Remove(item);

            Cart.ItemList = CartItems;
            Cart.TotalPrice = Cart.ItemList.Sum(m => m.Price);
            Cart.TotalItems = Cart.ItemList.Sum(m => m.Qty);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(Cart));
            return RedirectToAction("Index");
        }

    }
}
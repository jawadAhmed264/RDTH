using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RDTH.Data.Models;

namespace RDTH.ViewComponents
{
    public class CartTotalItems : ViewComponent
    {
        public CartTotalItems()
        {
        }
        public IViewComponentResult Invoke()
        {
            var cartSeesion = HttpContext.Session.GetString("Cart");
            int? total = 0;
            Cart cart = new Cart();
            if (!string.IsNullOrEmpty(cartSeesion))
            {
                cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
                total = cart.TotalItems;
            }

            ViewBag.TotalItems = total;
            return View();
        }
    }
}

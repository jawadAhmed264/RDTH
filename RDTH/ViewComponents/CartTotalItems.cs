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
            Cart cart;
            int? TotalItems = 0;

            if (TotalItems == null || cartSeesion=="")
            {
                TotalItems = 0;
            }
            if(cartSeesion!=null)
            {
                cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
                TotalItems = cart.TotalItems;
            }

            ViewBag.TotalItems = TotalItems;
            return View();
        }
    }
}

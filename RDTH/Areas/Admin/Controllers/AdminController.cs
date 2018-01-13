using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.AdminViewModel;
using RDTH.Data;
using System.Linq;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private RDTHDbContext _con;
        public AdminController(RDTHDbContext con)
        {
            _con = con;
        }

        [Route("Admin")]
        [Route("Admin/dashboard")]
        public IActionResult Index()
        {
            var payments = _con.Payments;
            var Recharges = _con.RechargeHistory.Include(r => r.Package).Include(r => r.CustomerCard);
            var Orders = _con.Orders.Include(o => o.Status).Include(o => o.Cart);
            var Feedbacks = _con.FeedBacks.Include(f => f.Status);
            var mod = _con.MoviesOnDemand.Include(m => m.Status);
            var Subscribes = _con.NewSubscribes.Include(s => s.Status);
            AdminIndexViewModel model = new AdminIndexViewModel()
            {
                Feedbacks = Feedbacks.OrderByDescending(f=>f.Id).Take(20),
                Orders = Orders.OrderByDescending(f => f.Id).Take(20),
                Payments = payments.OrderByDescending(f => f.Id).Take(20),
                RechargeCatalog = Recharges.OrderByDescending(f => f.Id).Take(20),
                LatestFeedbacks = Feedbacks.Where(f => f.Status.Name == "Pending").Count(),
                LatestOrders = Orders.Where(o => o.Status.Name == "Pending").Count(),
                LatestMOD = mod.Where(m => m.Status.Name == "Pending").Count(),
                LatestSubscribe = Subscribes.Where(s => s.Status.Name == "Pending").Count()
            };

            return View(model);
        }
    }
}
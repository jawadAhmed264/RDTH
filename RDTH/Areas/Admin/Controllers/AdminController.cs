using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.AdminViewModel;
using RDTH.Data;
using RDTH.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private RDTHDbContext _con;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(RDTHDbContext con, UserManager<ApplicationUser> userManager)
        {
            _con = con;
            _userManager = userManager;
        }

        [Route("Admin")]
        [Route("Admin/dashboard")]
        public IActionResult Index()
        {
            var payments = _con.Payments;
            var Recharges = _con.RechargeHistory.Include(r => r.Package).Include(r => r.CustomerCard);
            var Orders = _con.Orders.Include(o => o.Status).Include(o => o.Details);
            var Feedbacks = _con.FeedBacks.Include(f => f.Status);
            var request = _con.NewSetBoxRequest.Include(r => r.Status);
            var mod = _con.MoviesOnDemand.Include(m => m.Status);
            var Subscribes = _con.NewSubscribes.Include(s => s.Status);

            AdminIndexViewModel model = new AdminIndexViewModel()
            {
                Feedbacks = Feedbacks.OrderByDescending(f => f.Id).Take(20),
                Orders = Orders.OrderByDescending(f => f.Id).Take(20),
                Payments = payments.OrderByDescending(f => f.Id).Take(20),
                RechargeCatalog = Recharges.OrderByDescending(f => f.Id).Take(20),
                LatestRequest = request.Where(f => f.Status.Name == "Pending").Count(),
                LatestOrders = Orders.Where(o => o.Status.Name == "Pending").Count(),
                LatestMOD = mod.Where(m => m.Status.Name == "Pending").Count(),
                LatestSubscribe = Subscribes.Where(s => s.Status.Name == "Pending").Count()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(AddAdminModel model)
        {
            ApplicationUser testUser = await _userManager.FindByEmailAsync(model.Email);
            if (ModelState.IsValid)
            {
                if (testUser == null)
                {
                    ApplicationUser newUser = new ApplicationUser();
                    newUser.Email = model.Email;
                    newUser.UserName = model.Email;

                    string pass = model.Password;
                    IdentityResult result = await _userManager.CreateAsync(newUser, pass);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                        return RedirectToAction(nameof(Index));
                    }

                }
                ModelState.AddModelError("testUser", "Already a Member");
                return View(model);
            }
            return View(model);
        }
    }
}
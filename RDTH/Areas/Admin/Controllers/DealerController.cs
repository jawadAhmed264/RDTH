using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.DealerViewModel;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DealerController : Controller
    {
        private readonly RDTHDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DealerController(RDTHDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Dealer
        public async Task<IActionResult> Index()
        {
            var model = _context.Dealers.Include(d => d.ApplicationUser).Select(d => new DealerDetailModel()
            {
                Address = d.Address,
                City = d.City,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Id = d.Id,
                JoinDate = d.JoinDate,
                Telephone = d.Telephone,
                Email = d.ApplicationUser.Email
            });
            return View(await model.ToListAsync());
        }

        // GET: Admin/Dealer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .Include(d=>d.ApplicationUser)
                .SingleOrDefaultAsync(d=>d.Id == id);

            DealerDetailModel model = new DealerDetailModel
            {
                Id=dealer.Id,
                Address = dealer.Address,
                City = dealer.City,
                FirstName = dealer.FirstName,
                LastName = dealer.LastName,
                JoinDate = dealer.JoinDate,
                Telephone = dealer.Telephone,
                Email = dealer.ApplicationUser.Email
            };

            if (dealer == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Admin/Dealer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dealer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Telephone,Address,City,JoinDate", "Email", "Password")] DealerAddModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser testUser = await _userManager.FindByEmailAsync(model.Email);

                if (testUser == null)
                {
                    ApplicationUser newUser = new ApplicationUser();
                    newUser.Email = model.Email;
                    newUser.UserName = model.Email;

                    string pass = model.Password;
                    IdentityResult result = await _userManager.CreateAsync(newUser, pass);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Dealer");
                        Dealer dealer = new Dealer()
                        {
                            Address = model.Address,
                            City = model.City,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            JoinDate = DateTime.Now,
                            Telephone = model.Telephone,
                            ApplicationUser= newUser
                        };
                        _context.Add(dealer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                ModelState.AddModelError("testUser", "Already a Member");
                return View(model);

            }
            return View(model);
        }

        // GET: Admin/Dealer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers.SingleOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }
            return View(dealer);
        }

        // POST: Admin/Dealer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Telephone,Address,City,JoinDate")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerExists(dealer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dealer);
        }

        // GET: Admin/Dealer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // POST: Admin/Dealer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealer = await _context.Dealers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Dealers.Remove(dealer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerExists(int id)
        {
            return _context.Dealers.Any(e => e.Id == id);
        }
    }
}

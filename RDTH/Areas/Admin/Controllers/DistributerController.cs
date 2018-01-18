using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.DistributerViewModel;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DistributerController : Controller
    {
        private readonly RDTHDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DistributerController(RDTHDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Distributer
        public async Task<IActionResult> Index()
        {
            var model = _context.Distributers.Include(d => d.ApplicationUser).Select(d => new DistributerDetailModel()
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

        // GET: Admin/Distributer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributer = await _context.Distributers
                .Include(d => d.ApplicationUser)
                .SingleOrDefaultAsync(d => d.Id == id);

            DistributerDetailModel model = new DistributerDetailModel
            {
                Id = distributer.Id,
                Address = distributer.Address,
                City = distributer.City,
                FirstName = distributer.FirstName,
                LastName = distributer.LastName,
                JoinDate = distributer.JoinDate,
                Telephone = distributer.Telephone,
                Email = distributer.ApplicationUser.Email
            };

            if (distributer == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Admin/Distributer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Distributer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Telephone,Address,City,JoinDate", "Email", "Password")] DistributerAddModel model)
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
                        await _userManager.AddToRoleAsync(newUser, "Distributer");
                        Distributer distributer = new Distributer()
                        {
                            Address = model.Address,
                            City = model.City,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            JoinDate = DateTime.Now,
                            Telephone = model.Telephone,
                            ApplicationUser = newUser
                        };
                        _context.Add(distributer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                ModelState.AddModelError("testUser", "Already a Member");
                return View(model);

            }
            return View(model);
        }
        // GET: Admin/Distributer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributer = await _context.Distributers.SingleOrDefaultAsync(m => m.Id == id);
            if (distributer == null)
            {
                return NotFound();
            }
            return View(distributer);
        }

        // POST: Admin/Distributer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Telephone,Address,City,JoinDate")] Distributer distributer)
        {
            if (id != distributer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distributer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistributerExists(distributer.Id))
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
            return View(distributer);
        }

        // GET: Admin/Distributer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributer = await _context.Distributers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (distributer == null)
            {
                return NotFound();
            }

            return View(distributer);
        }

        // POST: Admin/Distributer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distributer = await _context.Distributers.SingleOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.FindByIdAsync(distributer.ApplicationUser.Id);
            if (user != null)
            {
                _context.Distributers.Remove(distributer);
                await _userManager.RemoveFromRoleAsync(user, "Distributer");
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DistributerExists(int id)
        {
            return _context.Distributers.Any(e => e.Id == id);
        }
    }
}

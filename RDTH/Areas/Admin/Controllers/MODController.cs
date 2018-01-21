using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.ModViewModel;
using RDTH.Data;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MODController : Controller
    {
        private readonly RDTHDbContext _context;

        public MODController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MOD
        public async Task<IActionResult> Index()
        {
            var movies = await _context.MoviesOnDemand.
                Include(m => m.Status).
                Include(m => m.Customer).
                OrderByDescending(m=>m.Id).
                Select(m => new ModDetailModel()
                {
                    Id = m.Id,
                    Movie = m.Movie,
                    MovieTime = m.MovieTime,
                    Status = m.Status,
                    Card = m.Customer.CustomerCard
                }).ToListAsync();

            var model = new ModIndexModel()
            {
                Mod = movies
            };
            return View(model);
        }



        // GET: Admin/MOD/Edit/5
        public async Task<IActionResult> Viewed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieOnDemand = await _context.MoviesOnDemand.SingleOrDefaultAsync(m => m.Id == id);
            if (movieOnDemand != null)
            {
                _context.Update(movieOnDemand);
                movieOnDemand.Status = _context.Status.SingleOrDefault(s => s.Name == "AdminApproved");
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }


        // GET: Admin/MOD/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieOnDemand = await _context.MoviesOnDemand
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movieOnDemand == null)
            {
                return NotFound();
            }

            return View(movieOnDemand);
        }

        // POST: Admin/MOD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieOnDemand = await _context.MoviesOnDemand.SingleOrDefaultAsync(m => m.Id == id);
            _context.MoviesOnDemand.Remove(movieOnDemand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieOnDemandExists(int id)
        {
            return _context.MoviesOnDemand.Any(e => e.Id == id);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SubscribeAdminController : Controller
    {
        private readonly RDTHDbContext _context;

        public SubscribeAdminController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SubscribeAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewSubscribes.
                Include(s=>s.Package).
                Include(s=>s.SetBox).
                Include(s=>s.Status).
                OrderByDescending(s=>s.ApplyDate).
                Where(s=>s.Status.Name!= "AdminApproved").
                ToListAsync());
        }

        // GET: Admin/SubscribeAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSubscribe = await _context.NewSubscribes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (newSubscribe == null)
            {
                return NotFound();
            }

            return View(newSubscribe);
        }

        // POST: Admin/SubscribeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newSubscribe = await _context.NewSubscribes.SingleOrDefaultAsync(m => m.Id == id);
            _context.NewSubscribes.Remove(newSubscribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewSubscribeExists(int id)
        {
            return _context.NewSubscribes.Any(e => e.Id == id);
        }
    }
}

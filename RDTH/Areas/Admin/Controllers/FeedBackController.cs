using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FeedBackController : Controller
    {
        private readonly RDTHDbContext _context;

        public FeedBackController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/FeedBack
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeedBacks.Include(m=>m.Status).ToListAsync());
        }


        // GET: Admin/FeedBack/Edit/5
        public async Task<IActionResult> Viewed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Feedback = await _context.FeedBacks.SingleOrDefaultAsync(m => m.Id == id);
            if (Feedback != null)
            {
                _context.Update(Feedback);
                Feedback.Status = _context.Status.SingleOrDefault(s => s.Name == "Viewed");
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: Admin/FeedBack/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // POST: Admin/FeedBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedBack = await _context.FeedBacks.SingleOrDefaultAsync(m => m.Id == id);
            _context.FeedBacks.Remove(feedBack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackExists(int id)
        {
            return _context.FeedBacks.Any(e => e.Id == id);
        }
    }
}

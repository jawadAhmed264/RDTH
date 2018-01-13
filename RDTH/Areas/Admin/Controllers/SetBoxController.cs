using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class SetBoxController : Controller
    {
        private readonly RDTHDbContext _context;

        public SetBoxController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SetBox
        public async Task<IActionResult> Index()
        {
            return View(await _context.SetBoxes.ToListAsync());
        }

        // GET: Admin/SetBox/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setBox = await _context.SetBoxes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setBox == null)
            {
                return NotFound();
            }

            return View(setBox);
        }

        // GET: Admin/SetBox/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SetBox/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specification,Price,ImageUrl")] SetBox setBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setBox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setBox);
        }

        // GET: Admin/SetBox/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setBox = await _context.SetBoxes.SingleOrDefaultAsync(m => m.Id == id);
            if (setBox == null)
            {
                return NotFound();
            }
            return View(setBox);
        }

        // POST: Admin/SetBox/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specification,Price,ImageUrl")] SetBox setBox)
        {
            if (id != setBox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetBoxExists(setBox.Id))
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
            return View(setBox);
        }

        // GET: Admin/SetBox/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setBox = await _context.SetBoxes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setBox == null)
            {
                return NotFound();
            }

            return View(setBox);
        }

        // POST: Admin/SetBox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setBox = await _context.SetBoxes.SingleOrDefaultAsync(m => m.Id == id);
            _context.SetBoxes.Remove(setBox);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetBoxExists(int id)
        {
            return _context.SetBoxes.Any(e => e.Id == id);
        }
    }
}

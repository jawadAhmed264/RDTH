using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using RDTH.Areas.Admin.Models.SetBoxViewModel;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SetBoxController : Controller
    {
        private readonly RDTHDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SetBoxController(RDTHDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/SetBox
        public async Task<IActionResult> Index()
        {
            return View(await _context.SetBoxes.OrderByDescending(s=>s.Id).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Name,Specification,Price,Image")] SetboxDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Image;

                var parsedContentDisposition =
                    ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var filename = Path.Combine(_hostingEnvironment.WebRootPath,
                    "images","setbox", parsedContentDisposition.FileName.ToString().Trim('"'));

                using (var stream = System.IO.File.OpenWrite(filename))
                {
                    await file.CopyToAsync(stream);
                }

                SetBox setBox = new SetBox()
                {
                    Name = model.Name,
                    ImageUrl =$"/images/setbox/{parsedContentDisposition.FileName.ToString().Trim('"')}",
                    Price = model.Price,
                    Specification = model.Specification

                };

                _context.Add(setBox);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specification,Price,ImageUrl")] SetBox model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetBoxExists(model.Id))
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
            return View(model);
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

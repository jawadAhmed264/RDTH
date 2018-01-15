using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using RDTH.Areas.Admin.Models.PackageViewModel;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly RDTHDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PackageController(RDTHDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Package
        public async Task<IActionResult> Index()
        {
            return View(await _context.Packages.ToListAsync());
        }

        // GET: Admin/Package/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .SingleOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Admin/Package/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Package/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageName,NoOfChannels,NewsChannel,EntertainmentChannel,SportsChannel,DocumentariesChannel,Charges,Image")] PackageDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Image;

                var parsedContentDisposition =
                    ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var filename = Path.Combine(_hostingEnvironment.WebRootPath,
                    "images", "package", parsedContentDisposition.FileName.ToString().Trim('"'));

                using (var stream = System.IO.File.OpenWrite(filename))
                {
                    await file.CopyToAsync(stream);
                }

                Package package = new Package
                {
                    Charges = model.Charges,
                    DocumentariesChannel = model.DocumentariesChannel,
                    EntertainmentChannel = model.EntertainmentChannel,
                    ImageUrl = $"/images/package/{parsedContentDisposition.FileName.ToString().Trim('"')}",
                    NewsChannel = model.NewsChannel,
                    NoOfChannels = model.NoOfChannels,
                    PackageName = model.PackageName,
                    SportsChannel = model.SportsChannel
                };

                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/Package/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages.SingleOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        // POST: Admin/Package/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PackageName,NoOfChannels,NewsChannel,EntertainmentChannel,SportsChannel,DocumentariesChannel,Charges,ImageUrl")] Package package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.Id))
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
            return View(package);
        }

        // GET: Admin/Package/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .SingleOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Admin/Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = await _context.Packages.SingleOrDefaultAsync(m => m.Id == id);
            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.Id == id);
        }
    }
}

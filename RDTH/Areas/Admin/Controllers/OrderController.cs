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
    public class OrderController : Controller
    {
        private readonly RDTHDbContext _context;

        public OrderController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.
                Include(m => m.Status).
                ToListAsync());
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.
                Include(m => m.Status).
                Include(m => m.Cart).
                SingleOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Approved(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders.SingleOrDefault(m=>m.Id==id);
            _context.Update(order);
            order.Status = _context.Status.SingleOrDefault(s=>s.Name== "AdminApproved");
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

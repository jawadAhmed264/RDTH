using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.Models.OrderViewModel;
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
                Include(m=>m.Details).
                OrderByDescending(m=>m.DatePlaced).
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
                Include(m => m.Details).
                FirstOrDefaultAsync(m => m.Id == id);

            var model = new OrderDetailModel {
                Id = order.Id,
                Contact = order.Contact,
                ShippingAddress = order.ShippingAddress,
                DatePlaced = order.DatePlaced,
                PersonName = order.PersonName,
                Status = order.Status.Name,
                TotalItems = order.TotalItems,
                TotalPrice = order.TotalPrice,
                Details = await _context.OrderDetails.Include(d => d.Product).Where(d => d.Order.Id == order.Id).ToListAsync(),
                Payment = await _context.Payments.Include(p=>p.Order).SingleOrDefaultAsync(p=>p.Order.Id==order.Id)
            };

            if (order == null)
            {
                return NotFound();
            }

            return View(model);
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
        public IActionResult Rejected(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders.SingleOrDefault(m => m.Id == id);
            _context.Update(order);
            order.Status = _context.Status.SingleOrDefault(s => s.Name == "AdminReject");
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

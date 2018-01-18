using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class PaymentController : Controller
    {
        private readonly RDTHDbContext _context;

        public PaymentController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Payment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payments.ToListAsync());
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}

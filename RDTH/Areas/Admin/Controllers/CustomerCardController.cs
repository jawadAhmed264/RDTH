using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.CustomClasses;
using RDTH.Areas.Admin.Models.CustomerCardViewModel;
using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerCardController : Controller
    {
        private readonly RDTHDbContext _context;

        public CustomerCardController(RDTHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerCard
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerCards.
                Include(c => c.Package).
                Include(c => c.SetBox)
                .ToListAsync());
        }

        // GET: Admin/CustomerCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCard = await _context.CustomerCards
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customerCard == null)
            {
                return NotFound();
            }

            return View(customerCard);
        }

        // GET: Admin/CustomerCard/Create
        public async Task<IActionResult> Create(int id)
        {
            var Subscriber =await _context.NewSubscribes.
                Include(s => s.Package).
                Include(s => s.SetBox).
                SingleOrDefaultAsync(s => s.Id == id);

            _context.Update(Subscriber);
            Subscriber.Status =await _context.Status.FirstOrDefaultAsync(st => st.Name == "Viewed");
            _context.SaveChanges();

            CardAddModel card = new CardAddModel()
            {
                Address = Subscriber.Address,
                CardNumber = CardNumberGenerator.CardNumber(),
                ContactNumber = Subscriber.ContactNumber,
                OwnerName = Subscriber.OwnerName,
                PackageId = Subscriber.Package.Id,
                SetBoxId = Subscriber.SetBox.Id,
                SubscribeDate = DateTime.Now
            };
            return View(card);
        }

        // POST: Admin/CustomerCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerName,ContactNumber,Address,CardNumber,SubscribeDate", "PackageId", "SetBoxId")] CardAddModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerCard customerCard = new CustomerCard()
                {
                    Address = model.Address,
                    SubscribeDate = model.SubscribeDate,
                    CardNumber = model.CardNumber,
                    ContactNumber = model.ContactNumber,
                    OwnerName = model.OwnerName,
                    Package =await _context.Packages.FirstOrDefaultAsync(p => p.Id == model.PackageId),
                    SetBox =await _context.SetBoxes.FirstOrDefaultAsync(s => s.Id == model.SetBoxId)
                };

                CustomerPackage cp = new CustomerPackage
                {
                    CustomerCard = customerCard,
                    NumberOfMonths = 0,
                    ExpirationDate = DateTime.Now,
                    Package =await _context.Packages.FirstOrDefaultAsync(p => p.Id == model.PackageId),
                    Status =await _context.Status.SingleOrDefaultAsync(s => s.Name == "Recharged")
                };

                NewSetBoxRequest request = new NewSetBoxRequest()
                {
                    Card=customerCard,
                    Setbox=await _context.SetBoxes.FirstOrDefaultAsync(s => s.Id == model.SetBoxId),
                    Status=await _context.Status.SingleOrDefaultAsync(s => s.Name == "AdminApproved")
                };

                await _context.AddAsync(customerCard);
                await _context.AddAsync(cp);
                await _context.AddAsync(request);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/CustomerCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SetDropDown();
            var customerCard = await _context.CustomerCards.
                Include(c => c.Package).Include(c => c.SetBox).
                SingleOrDefaultAsync(m => m.Id == id);

            var model = new CardAddModel
            {
                Address = customerCard.Address,
                ContactNumber = customerCard.ContactNumber,
                Id = customerCard.Id,
                OwnerName = customerCard.OwnerName,
                CardNumber = customerCard.CardNumber,
                PackageId = _context.Packages.FirstOrDefault(p => p.Id == customerCard.Package.Id).Id,
                SetBoxId = _context.SetBoxes.FirstOrDefault(p => p.Id == customerCard.SetBox.Id).Id,
            };

            if (customerCard == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Admin/CustomerCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerName,ContactNumber,Address,CardNumber,SubscribeDate,PackageId,SetBoxId")] CardAddModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CustomerCard customerCard = await _context.CustomerCards.
                      Include(c => c.Package).Include(c => c.SetBox).
                      SingleOrDefaultAsync(m => m.Id == id);

                    _context.Update(customerCard);

                    customerCard.OwnerName = model.OwnerName;
                    customerCard.SubscribeDate = model.SubscribeDate;
                    customerCard.CardNumber = model.CardNumber;
                    customerCard.ContactNumber = model.ContactNumber;
                    customerCard.Address = model.Address;
                    customerCard.Package = _context.Packages.FirstOrDefault(p => p.Id == model.PackageId);
                    customerCard.SetBox = _context.SetBoxes.FirstOrDefault(s => s.Id == model.SetBoxId);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerCardExists(model.Id))
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

        private bool CustomerCardExists(int id)
        {
            return _context.CustomerCards.Any(e => e.Id == id);
        }

        private void SetDropDown()
        {
            ViewBag.Packages = _context.Packages.ToList();
            ViewBag.SetBoxes = _context.SetBoxes.ToList();
        }
    }
}

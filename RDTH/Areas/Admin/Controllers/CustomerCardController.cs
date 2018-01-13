﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Areas.Admin.CustomClasses;
using RDTH.Areas.Admin.Models.CustomerCardViewModel;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
            return View(await _context.CustomerCards.ToListAsync());
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
        public IActionResult Create(int id)
        {
            var Subscriber = _context.NewSubscribes.
                Include(s => s.Package).
                Include(s => s.SetBox).
                SingleOrDefault(s => s.Id == id);

            _context.Update(Subscriber);
            Subscriber.Status = _context.Status.FirstOrDefault(st=>st.Name== "Viewed");
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
                    Package = _context.Packages.FirstOrDefault(p => p.Id == model.PackageId),
                    SetBox = _context.SetBoxes.FirstOrDefault(s => s.Id == model.SetBoxId)
                };

                _context.Add(customerCard);
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

            var customerCard = await _context.CustomerCards.SingleOrDefaultAsync(m => m.Id == id);
            if (customerCard == null)
            {
                return NotFound();
            }
            return View(customerCard);
        }

        // POST: Admin/CustomerCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerName,ContactNumber,Address,CardNumber,SubscribeDate")] CustomerCard customerCard)
        {
            if (id != customerCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerCardExists(customerCard.Id))
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
            return View(customerCard);
        }

        // GET: Admin/CustomerCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/CustomerCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerCard = await _context.CustomerCards.SingleOrDefaultAsync(m => m.Id == id);
            _context.CustomerCards.Remove(customerCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerCardExists(int id)
        {
            return _context.CustomerCards.Any(e => e.Id == id);
        }
    }
}
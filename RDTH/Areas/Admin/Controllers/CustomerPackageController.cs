using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using System;
using System.Linq;

namespace RDTH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CustomerPackageController : Controller
    {
        private readonly RDTHDbContext _con;

        public CustomerPackageController(RDTHDbContext context)
        {
            _con = context;
        }

        [HttpPost]
        public IActionResult UpdateStatus()
        {
            DateTime now = DateTime.Now;

            var status = _con.Status.
                SingleOrDefault(m => m.Name == "Recharged");

            var cp = _con.CustomerPackages.
                Include(c => c.Status).
                Where(c => c.ExpirationDate < now).ToList();

            cp.ForEach(c => c.Status = status);
            _con.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}
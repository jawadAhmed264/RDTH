using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class PackageService : IPackageService
    {
        private RDTHDbContext _context;
        public PackageService(RDTHDbContext context)
        {
            _context = context;
        }
        public void AddPackage(Package newPackage)
        {
            _context.Packages.Add(newPackage);
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages;
        }

        public Package GetById(int id)
        {
            return GetAll().FirstOrDefault(p=>p.Id==id);
        }

        public IEnumerable<Package> GetLatest()
        {
            return GetAll().TakeLast(3);
        }
    }
}

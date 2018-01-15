using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class CustomerPackageService : ICustomerPackage
    {
        private readonly RDTHDbContext _con;

        public CustomerPackageService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(CustomerPackage newCp)
        {
            _con.Add(newCp);
            _con.SaveChanges();
        }

        public IEnumerable<CustomerPackage> GetAll()
        {
            return _con.CustomerPackages.
                Include(cp => cp.Customer).
                Include(cp => cp.Package).
                Include(cp => cp.Status).
                Include(cp => cp.CustomerCard);
        }

        public CustomerPackage GetByCardId(int CardId)
        {
            return GetAll().SingleOrDefault(cp => cp.CustomerCard.Id == CardId);
        }

        public CustomerPackage GetById(int customerId)
        {
            return GetAll().FirstOrDefault(cp => cp.Customer.Id == customerId);
        }

        public DateTime GetExpirationTime(int customerId)
        {
            return GetAll().
                FirstOrDefault(cp => cp.Customer.Id == customerId).
                ExpirationDate;
        }

        public Package GetPackage(int customerId)
        {
            return GetAll().
                FirstOrDefault(cp => cp.Customer.Id == customerId).
                Package;
        }

        public Status GetRechargeStatus(int customerId)
        {
            return GetAll().
                FirstOrDefault(cp => cp.Customer.Id == customerId).
                Status;
        }

        public void Update(CustomerPackage newCp)
        {
            _con.Update(newCp);
            _con.SaveChanges();
        }
    }
}

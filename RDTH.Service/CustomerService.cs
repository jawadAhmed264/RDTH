﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class CustomerService : ICustomer
    {
        private readonly RDTHDbContext _con;
        public CustomerService(RDTHDbContext con)
        {
            _con = con;
        }
        public IEnumerable<Customer> GetAll()
        {
            return _con.Customers.Include(c=>c.ApplicationUser);
        }

        public Customer GetById(int id)
        {
            return GetAll().FirstOrDefault(c=>c.Id==id);
        }

        public Customer GetByUser(string UserId)
        {
            return GetAll().FirstOrDefault(c => c.ApplicationUser.Id == UserId);
        }
    }
}
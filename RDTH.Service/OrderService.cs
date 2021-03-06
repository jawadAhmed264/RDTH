﻿using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class OrderService : IOrder
    {
        private readonly RDTHDbContext _con;

        public OrderService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Order newOrder)
        {
            _con.Add(newOrder);
            _con.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _con.Orders.
                Include(o=>o.Status).
                Include(o=>o.Details);
        }

        public Order GetById(int OrderId)
        {
            return GetAll().FirstOrDefault(o=>o.Id==OrderId);
        }
    }
}

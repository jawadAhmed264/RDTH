using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class DealerService : IDealerService
    {
        private RDTHDbContext _con;

        public DealerService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Dealer newDealer)
        {
            _con.Dealers.Add(newDealer);
        }

        public IEnumerable<Dealer> GetAll()
        {
            return _con.Dealers.Include(m=>m.Orders).
                Include(m=>m.Payments).Include(m=>m.ApplicationUser);
        }

        public Dealer GetById(int DealerId)
        {
            return GetAll().FirstOrDefault(d=>d.Id==DealerId);
        }

        public Dealer GetByUserId(string UserId)
        {
            return GetAll().SingleOrDefault(m => m.ApplicationUser.Id == UserId);
        }

        public IEnumerable<Order> GetOrders(int DealerId)
        {
            return GetById(DealerId).Orders;
        }

        public IEnumerable<Payment> GetPayments(int DealerId)
        {
            return GetById(DealerId).Payments;
        }
    }
}

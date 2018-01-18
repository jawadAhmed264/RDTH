using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class DistributerServicecs : IDistributerService
    {
        private readonly RDTHDbContext _con;

        public DistributerServicecs(RDTHDbContext con)
        {
            _con = con;
        }
        public IEnumerable<Distributer> GetAll()
        {
           return _con.Distributers.
                Include(d=>d.Orders).
                Include(d=>d.Payments).
                Include(d=>d.ApplicationUser);
        }

        public Distributer GetById(int Id)
        {
            return GetAll().FirstOrDefault(d=>d.Id==Id);
        }

        public Distributer GetByUserId(string UserId)
        {
            return GetAll().SingleOrDefault(m => m.ApplicationUser.Id == UserId);
        }

        public IEnumerable<Order> GetOrders(int DisId)
        {
            return GetById(DisId).Orders;
        }

        public IEnumerable<Payment> GetPayments(int DisId)
        {
            return GetById(DisId).Payments;
        }
    }
}

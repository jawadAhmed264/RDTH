using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class PaymentService : IPayment
    {
        private readonly RDTHDbContext _con;

        public PaymentService(RDTHDbContext con)
        {
            _con = con;
        }
        public void Add(Payment newPayment)
        {
            _con.Add(newPayment);
            _con.SaveChanges();
        }

        public IEnumerable<Payment> GetAll()
        {
            return _con.Payments.Include(p=>p.Order);
        }

        public Payment GetById(int PaymentId)
        {
            return GetAll().FirstOrDefault(p=>p.Id==PaymentId);
        }
    }
}

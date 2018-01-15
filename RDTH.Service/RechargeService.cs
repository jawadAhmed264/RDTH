using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class RechargeService : IRechargeService
    {
        private readonly RDTHDbContext _con;

        public RechargeService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Recharge newRecharge)
        {
            _con.Add(newRecharge);
            _con.SaveChanges();
        }

        public IEnumerable<Recharge> GetAll()
        {
            return _con.RechargeHistory.
                Include(r=>r.CustomerCard).Include(r=>r.Package);
        }

        public Recharge GetById(int Id)
        {
            return GetAll().FirstOrDefault(r=>r.Id==Id);
        }

        public IEnumerable<Recharge> GetByCustomerCard(string cardNumber)
        {
            return GetAll().Where(r => r.CustomerCard.CardNumber == cardNumber);
        }
    }
}

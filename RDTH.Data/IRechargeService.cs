using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IRechargeService
    {
        IEnumerable<Recharge> GetAll();
        IEnumerable<Recharge> GetByCustomerCard(string cardNumber);
        Recharge GetById(int Id);
        void Add(Recharge newRecharge);
    }
}

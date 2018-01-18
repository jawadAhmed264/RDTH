using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IDealerService
    {
        IEnumerable<Dealer> GetAll();
        IEnumerable<Order> GetOrders(int DealerId);
        IEnumerable<Payment> GetPayments(int DealerId);
        Dealer GetById(int DealerId);
        Dealer GetByUserId(string UserId);
        void Add(Dealer newDealer);

    }
}

using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<int> PlacedOrderAsync(Dealer dealer, Cart cart, Order newOrder, Payment newPayment);
    }
}

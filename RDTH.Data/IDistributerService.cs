using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDTH.Data
{
    public interface IDistributerService
    {
        IEnumerable<Distributer> GetAll();
        IEnumerable<Order> GetOrders(int DisId);
        IEnumerable<Payment> GetPayments(int DisId);
        Distributer GetByUserId(string UserId);
        Distributer GetById(int Id);
        Task<int> PlacedOrderAsync(Distributer dis, Cart cart, Order newOrder, Payment newPayment);
    }
}

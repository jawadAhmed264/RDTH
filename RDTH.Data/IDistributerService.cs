using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IDistributerService
    {
        IEnumerable<Distributer> GetAll();
        IEnumerable<Order> GetOrders(int DisId);
        IEnumerable<Payment> GetPayments(int DisId);
        Distributer GetByUserId(string UserId);
        Distributer GetById(int Id);
    }
}

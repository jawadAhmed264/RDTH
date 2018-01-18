using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IOrder
    {
        IEnumerable<Order> GetAll();
        Order GetById(int OrderId);
        void Add(Order newOrder);
    }
}

using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Customer GetByUser(string UserId);
        bool IsCustomer(int CardId);

        void Add(Customer newCustomer);
    }
}

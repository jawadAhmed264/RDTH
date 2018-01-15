using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface ICardService
    {
        IEnumerable<CustomerCard> GetAll();
        CustomerCard GetCurrentUserCard(string UserId);
        CustomerCard GetCardByNumber(string cardNumber);
        bool CheckCard(string Customercard);
        void Update(CustomerCard card);
    }
}

using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class CardService : ICardService
    {
        private RDTHDbContext _con;

        public CardService(RDTHDbContext con)
        {
            _con = con;
        }

        public bool CheckCard(string Customercard)
        {
            return _con.CustomerCards.Any(c => c.CardNumber == Customercard);
        }

        public IEnumerable<CustomerCard> GetAll()
        {
            return _con.CustomerCards.
                Include(p => p.Package).
                Include(p => p.SetBox);
        }

        public CustomerCard GetCardByNumber(string cardNumber)
        {
            return GetAll().SingleOrDefault(c => c.CardNumber == cardNumber);
        }

        public CustomerCard GetCurrentUserCard(string UserId)
        {
            var customer = _con.Customers.
             Include(c=>c.CustomerCard).
             Include(c => c.ApplicationUser).
             FirstOrDefault(c => c.ApplicationUser.Id == UserId);

            return _con.CustomerCards.
                Include(card=>card.Package).
                Include(card=>card.SetBox).
                FirstOrDefault(card=>card.Id==customer.CustomerCard.Id);
        }

        public void Update(CustomerCard card)
        {
            _con.Update(card);
            _con.SaveChanges();
        }
    }
}

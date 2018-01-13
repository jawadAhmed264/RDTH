using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System;
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
        public CustomerCard GetCurrentUserCard(string Id)
        {
            var customer = _con.Customers.
             Include(c=>c.CustomerCard).
             Include(c => c.ApplicationUser).
             FirstOrDefault(c => c.ApplicationUser.Id == Id);

            return _con.CustomerCards.
                Include(card=>card.Package).
                Include(card=>card.SetBox).
                FirstOrDefault(card=>card.Id==customer.CustomerCard.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class NewSetBoxRequestService : INewSetBoxRequest
    {
        private readonly RDTHDbContext _con;

        public NewSetBoxRequestService(RDTHDbContext con)
        {
            _con = con;
        }
        public void Add(NewSetBoxRequest newRequest)
        {
            _con.Add(newRequest);
            _con.SaveChanges();
        }

        public bool CheckAlreadyApplied(string cardNumber)
        {
            return GetByCard(cardNumber).
                Where(r=>r.Status.Name=="Pending" || r.Status.Name == "AdminApproved").
                Any(r=>r.Card.CardNumber==cardNumber);
        }

        public IEnumerable<NewSetBoxRequest> GetAll()
        {
            return _con.NewSetBoxRequest.
                Include(r => r.Card).
                Include(r => r.Setbox).
                Include(r => r.Status).
                OrderByDescending(r => r.Id);
        }

        public IEnumerable<NewSetBoxRequest> GetByCard(string card)
        {
            return GetAll().Where(r => r.Card.CardNumber == card);
        }

        public NewSetBoxRequest GetById(int? Id)
        {
            return GetAll().FirstOrDefault(r => r.Id == Id);
        }

        public IEnumerable<NewSetBoxRequest> GetBySetBox(string setBoxName)
        {
            return GetAll().Where(r => r.Setbox.Name == setBoxName);
        }

        public IEnumerable<NewSetBoxRequest> GetByStatus(string status)
        {
            return GetAll().Where(r => r.Status.Name == status);
        }

        public void Remove(NewSetBoxRequest request)
        {
            _con.Remove(request);
            _con.SaveChanges();
        }

        public void Update(NewSetBoxRequest request)
        {
            _con.Update(request);
            _con.SaveChanges();
        }
    }
}

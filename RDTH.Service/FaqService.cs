using System.Collections.Generic;
using System.Linq;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class FaqService : IFaq
    {
        private RDTHDbContext _con;

        public FaqService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Faq newFaq)
        {
            _con.Faqs.Add(newFaq);
            _con.SaveChanges();
        }
        public IEnumerable<Faq> GetAll()
        {
            return _con.Faqs;
        }
        public IEnumerable<Faq> GetRecent()
        {
            return GetAll().TakeLast(10);
        }
        public Faq GetById(int id)
        {
            return GetAll().FirstOrDefault(faq => faq.Id == id);
        }
    }
}

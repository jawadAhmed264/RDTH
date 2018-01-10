using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IFaq
    {
        IEnumerable<Faq> GetAll();
        IEnumerable<Faq> GetRecent();
        Faq GetById(int id);
        void Add(Faq newFaq);
    }
}

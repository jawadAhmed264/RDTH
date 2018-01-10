using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class DealerService : IDealerService
    {
        private RDTHDbContext _con;

        public DealerService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Dealer newDealer)
        {
            _con.Dealers.Add(newDealer);
        }

        public IEnumerable<Dealer> GetAll()
        {
            return _con.Dealers;
        }

        public Dealer GetById(int DealerId)
        {
            return GetAll().FirstOrDefault(d=>d.Id==DealerId);
        }
    }
}

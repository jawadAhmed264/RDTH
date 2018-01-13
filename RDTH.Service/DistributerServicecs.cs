using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class DistributerServicecs : IDistributerService
    {
        private readonly RDTHDbContext _con;

        public DistributerServicecs(RDTHDbContext con)
        {
            _con = con;
        }
        public IEnumerable<Distributer> GetAll()
        {
           return _con.Distributers;
        }

        public Distributer GetById(int Id)
        {
            return GetAll().FirstOrDefault(d=>d.Id==Id);
        }
    }
}

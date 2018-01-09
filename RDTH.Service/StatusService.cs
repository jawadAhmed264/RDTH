using System.Collections.Generic;
using System.Linq;
using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class StatusService:IStatusService
    {
        private RDTHDbContext _context;

        public StatusService(RDTHDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Status;
        }

        public Status GetById(int id)
        {
            return GetAll().FirstOrDefault(s=>s.Id==id);
        }

        public Status GetByName(string name)
        {
            return GetAll().FirstOrDefault(s => s.Name == name);
        }
    }
}

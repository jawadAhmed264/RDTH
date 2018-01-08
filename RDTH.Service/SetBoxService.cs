using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTH.Service
{
    public class SetBoxService : ISetBoxService
    {
        private RDTHDbContext _context;

        public SetBoxService(RDTHDbContext context)
        {
            _context = context;
        }

        public void AddSetBox(SetBox newDevice)
        {
            _context.SetBoxes.Add(newDevice);
        }

        public IEnumerable<SetBox> GetAll()
        {
            return _context.SetBoxes;
        }

        public SetBox GetById(int id)
        {
            return GetAll().FirstOrDefault(sb=>sb.Id==id);
        }

        public IEnumerable<SetBox> GetLatest()
        {
            return GetAll().TakeLast(3);
        }
    }
}

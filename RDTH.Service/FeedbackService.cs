using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RDTH.Service
{
    public class FeedbackService : IFeedback
    {
        private RDTHDbContext _context;
        public FeedbackService(RDTHDbContext context)
        {
            _context = context;
        }
        public void Add(FeedBack newFeedback)
        {
            _context.Add(newFeedback);
            _context.SaveChanges();
        }

        public IEnumerable<FeedBack> GetAll()
        {
            return _context.FeedBacks;
        }

        public FeedBack GetById(int id)
        {
            return GetAll().FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<FeedBack> GetLatestFeedback()
        {
            return GetAll().TakeLast(10);
        }
    }
}

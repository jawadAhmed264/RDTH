using RDTH.Data;
using RDTH.Data.Models;

namespace RDTH.Service
{
    public class SubscribeService : ISubscribeService
    {
        private RDTHDbContext _context;
        public SubscribeService(RDTHDbContext context) {
            _context = context;
        }
        public void AddSubscribe(NewSubscribe newSub)
        {
            _context.NewSubscribes.Add(newSub);
            _context.SaveChanges();
        }
    }
}

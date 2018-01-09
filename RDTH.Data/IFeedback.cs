using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IFeedback
    {
        IEnumerable<FeedBack> GetAll();
        IEnumerable<FeedBack> GetLatestFeedback();
        FeedBack GetById(int id);
        void Add(FeedBack newFeedback);
    }
}

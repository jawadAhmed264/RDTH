using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IDealerService
    {
        IEnumerable<Dealer> GetAll();
        Dealer GetById(int DealerId);
        void Add(Dealer newDealer);

    }
}

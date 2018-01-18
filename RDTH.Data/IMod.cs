using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IMod
    {
        IEnumerable<MovieOnDemand> GetAll();
        IEnumerable<MovieOnDemand> GetByCustomer(int CustomerID);
        MovieOnDemand GetById(int id);
        void Add(MovieOnDemand newMovie);

    }
}

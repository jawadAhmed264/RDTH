using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IDistributerService
    {
        IEnumerable<Distributer> GetAll();
        Distributer GetById(int Id);
    }
}

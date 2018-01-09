using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IStatusService
    {
        IEnumerable<Status> GetAll();
        Status GetById(int id);
        Status GetByName(string name);
    }
}

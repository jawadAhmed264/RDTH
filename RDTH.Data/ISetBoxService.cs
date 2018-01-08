using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface ISetBoxService
    {
        IEnumerable<SetBox> GetAll();
        IEnumerable<SetBox> GetLatest();
        SetBox GetById(int id);
        void AddSetBox(SetBox newDevice);
    }
}

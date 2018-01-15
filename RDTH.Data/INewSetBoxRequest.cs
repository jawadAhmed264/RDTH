using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface INewSetBoxRequest
    {
        IEnumerable<NewSetBoxRequest> GetAll();
        IEnumerable<NewSetBoxRequest> GetByStatus(string status);
        IEnumerable<NewSetBoxRequest> GetBySetBox(string setBoxName);
        IEnumerable<NewSetBoxRequest> GetByCard(string card);

        NewSetBoxRequest GetById(int? Id);
        void Add(NewSetBoxRequest newRequest);
        void Update(NewSetBoxRequest request);
        void Remove(NewSetBoxRequest request);
        bool CheckAlreadyApplied(string cardNumber);
    }
}

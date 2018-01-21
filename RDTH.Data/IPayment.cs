using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IPayment
    {
       IEnumerable<Payment> GetAll();
       Payment GetById(int PaymentId);
       void Add(Payment newPayment);
    }
}

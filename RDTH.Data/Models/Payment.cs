using System;

namespace RDTH.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PersonName { get; set; }
        public string Contact { get; set; }
        public string PaymentType { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime CardExpiry { get; set; }
        public string CVV { get; set; }
        public decimal Cost { get; set; }
        //navigational property

        public virtual Order Order { get; set; }
        public virtual Dealer Dealer { get; set; }
        public virtual Distributer Distributer { get; set; }
    }
}

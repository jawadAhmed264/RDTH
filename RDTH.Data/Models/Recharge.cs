using System;
using System.Collections.Generic;
using System.Text;

namespace RDTH.Data.Models
{
    public class Recharge
    {
        public int Id { get; set; }
        public DateTime RechargeDate { get; set; }
        public string PaymentType { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime CardExpiry { get; set; }
        public string CVV { get; set; }
        public decimal Cost { get; set; }

        public virtual Package Package { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }
    }
}

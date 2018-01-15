using System;

namespace RDTH.Models.RechargeViewModel
{
    public class RechargeHistoryDetailModel
    {
     
        public string PaymentType { get; set; }
        public string Customercard { get; set; }
        public string OwnerName { get; set; }
        public DateTime RechargeDate { get; set; }
        public string PackageName { get; set; }
        public decimal Charges { get; set; }
        public decimal Total { get; set; }
    }
}

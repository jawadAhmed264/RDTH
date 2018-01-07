using System;
using System.Collections.Generic;
using System.Text;

namespace RDTH.Data.Models
{
    public class CustomerCard
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string CardNumber { get; set; }
        public DateTime SubscribeDate { get; set; }

        //navigational Properties
        public virtual Package Package { get; set; }
        public virtual SetBox SetBox { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
        public virtual IEnumerable<Recharge> RechargeHistory { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<MovieOnDemand> MoviesOnDemand { get; set; }

    }
}

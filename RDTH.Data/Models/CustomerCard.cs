using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Data.Models
{
    public class CustomerCard
    {
        public int Id { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string CardNumber { get; set; }
        public DateTime SubscribeDate { get; set; }

        //navigational Properties
        public virtual Package Package { get; set; }
        public virtual SetBox SetBox { get; set; }
        public virtual IEnumerable<Recharge> RechargeHistory { get; set; }
    }
}

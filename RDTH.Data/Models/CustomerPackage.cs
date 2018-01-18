using System;

namespace RDTH.Data.Models
{
    public class CustomerPackage
    {
        public int Id { get; set; }
        public int NumberOfMonths { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }
        //navigational properties
        public virtual Customer Customer { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }
        public virtual Package Package { get; set; }

    }
}

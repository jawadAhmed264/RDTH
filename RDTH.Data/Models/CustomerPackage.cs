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
        public Customer Customer { get; set; }
        public CustomerCard CustomerCard { get; set; }
        public Package Package { get; set; }

    }
}

using System;

namespace RDTH.Data.Models
{
    public class NewSubscribe
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime ApplyDate { get; set; }
        public Status Status { get; set; }
    }
}

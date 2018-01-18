using System;

namespace RDTH.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public string PersonName { get; set; }
        public string Contact { get; set; }
        public string ShippingAddress { get; set; }
        public Status Status { get; set; }

        public virtual Cart Cart { get; set; }
    }
}

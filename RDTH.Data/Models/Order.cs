using System;

namespace RDTH.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public Status Status { get; set; }

        public Cart Cart { get; set; }
    }
}

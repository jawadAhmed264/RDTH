using System;
using System.Collections.Generic;

namespace RDTH.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public string PersonName { get; set; }
        public string Contact { get; set; }
        public string ShippingAddress { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }

        public virtual IEnumerable<OrderDetail> Details { get; set; }
        public virtual Dealer Dealer { get; set; }
        public virtual Distributer Distributer { get; set; }
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }

        public virtual SetBox Product { get; set; }
        public virtual Order Order { get; set; }
    }
}

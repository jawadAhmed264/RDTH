using RDTH.Data.Models;
using System;
using System.Collections.Generic;

namespace RDTH.Areas.Admin.Models.OrderViewModel
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public string PersonName { get; set; }
        public string Contact { get; set; }
        public string ShippingAddress { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public virtual IEnumerable<OrderDetail> Details { get; set; }
        public virtual Payment Payment { get; set; }

    }
}

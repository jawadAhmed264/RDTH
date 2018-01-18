using System.Collections.Generic;

namespace RDTH.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual List<CartItem> ItemList { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public virtual SetBox Product { get; set; }
    }
}

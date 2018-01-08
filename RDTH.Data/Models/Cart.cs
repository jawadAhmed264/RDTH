using System.Collections.Generic;

namespace RDTH.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}

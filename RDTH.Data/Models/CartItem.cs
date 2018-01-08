namespace RDTH.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public SetBox Product { get; set; }
        public decimal Total { get; set; }

        public virtual Cart Cart { get; set; }
    }
}

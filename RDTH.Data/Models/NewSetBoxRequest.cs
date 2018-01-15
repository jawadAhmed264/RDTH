namespace RDTH.Data.Models
{
    public class NewSetBoxRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public CustomerCard Card { get; set; }
        public SetBox Setbox { get; set; }

    }
}

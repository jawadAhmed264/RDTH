using System;

namespace RDTH.Data.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
    }
}

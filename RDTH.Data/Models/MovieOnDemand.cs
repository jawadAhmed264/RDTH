using System;

namespace RDTH.Data.Models
{
    public class MovieOnDemand
    {
        public int Id { get; set; }
        public string Movie { get; set; }
        public DateTime MovieTime { get; set; }
        public Status Status { get; set; }
        //navigational Properties
        public virtual Customer Customer { get; set; }
    }
}

using RDTH.Models;
using System.Collections.Generic;

namespace RDTH.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        //navigation properties
        public virtual CustomerCard CustomerCard { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual IEnumerable<MovieOnDemand> MoviesOnDemand { get; set; }
    }
}

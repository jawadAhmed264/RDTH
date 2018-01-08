using RDTH.Models;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Models
{
    public class Distributer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime JoinDate { get; set; }

        //navigational properties
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}

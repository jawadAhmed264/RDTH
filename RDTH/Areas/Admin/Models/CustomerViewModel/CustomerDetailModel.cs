using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Areas.Admin.Models.CustomerViewModel
{
    public class CustomerDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Card { get; set; }
    }
}

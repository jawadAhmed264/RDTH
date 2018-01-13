using System;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.ModViewModel
{
    public class ModAddModel
    {
        [Required]
        public string Movie { get; set; }
        [Required]
        public DateTime MovieTime { get; set; }
    }
}

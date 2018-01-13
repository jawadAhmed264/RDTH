using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Areas.Admin.Models.DistributerViewModel
{
    public class DistributerDetailModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "input only Alphabets")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "input only Alphabets")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Telephone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Not a valid Phone")]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "input only Alphabets")]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Areas.Admin.Models.CustomerCardViewModel
{
    public class CardAddModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Your Name")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "input only Alphabets")]
        public string OwnerName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Not a valid Phone")]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string CardNumber { get; set; }

        public DateTime SubscribeDate { get; set; }

        [Display(Name = "Package")]
        public int PackageId { get; set; }

        [Display(Name = "SetBox Device")]
        public int SetBoxId { get; set; }
    }
}

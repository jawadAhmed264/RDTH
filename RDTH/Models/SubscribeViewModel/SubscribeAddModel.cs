using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.SubscribeViewModel
{
    public class SubscribeAddModel
    {
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

        [Display(Name ="Package")]
        public int PackageId { get; set; }

        [Display(Name = "SetBox Device")]
        public int SetBoxId { get; set; }
    }
}

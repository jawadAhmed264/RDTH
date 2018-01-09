using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.FeedbackViewModel
{
    public class FeedbackAddModel
    {
        [Required]
        [StringLength(30,MinimumLength =3)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "input only Alphabets")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Msg { get; set; }
    }
}

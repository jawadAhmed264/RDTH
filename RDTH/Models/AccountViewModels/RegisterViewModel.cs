using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote("IsCardAvailable", "Recharge", ErrorMessage = "Customer Card Invalid")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Input Card Pattern (XXXX-XXXX-XXXX)")]
        [Display(Name = "Your Customer Card Number")]
        public string Customercard { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

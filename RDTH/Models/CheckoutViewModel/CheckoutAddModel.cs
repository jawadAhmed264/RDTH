using RDTH.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.CheckoutViewModel
{
    public class CheckoutAddModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Input alphabets only")]
        [Display(Name = "Full Name")]
        public string PersonName { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 7)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input Numbers only")]
        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Required]
        [Display(Name ="Shipping Address")]
        [DataType(DataType.MultilineText)]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Maximum 16 Digits")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input numbers only")]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Credit Card Expiration")]
        public DateTime CardExpiry { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input numbers only")]
        [StringLength(3, ErrorMessage = "three digit cvv Number of your credit Card")]
        public string CVV { get; set; }


        public Cart Cart { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Models.RechargeViewModel
{
    public class RechargeAddModel
    {
        [Required]
        [Display(Name ="Payment Type")]
        public string PaymentType { get; set; }

        [Required]
        [Remote("IsCardAvailable","Recharge",ErrorMessage ="Customer Card Invalid")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Input Card Pattern (XXXX-XXXX-XXXX)")]
        [Display(Name = "Your Customer Card Number")]
        public string Customercard { get; set; }

        [Required]
        [StringLength(16,MinimumLength =16,ErrorMessage ="Maximum 16 Digits")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input numbers only")]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Credit Card Expiration")]
        public DateTime CardExpiry { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input numbers only")]
        [StringLength(3,ErrorMessage ="three digit cvv Number of your credit Card")]
        public string CVV { get; set; }

        [Required]
        public int Months { get; set; }

        [Required]
        public decimal Charges { get; set; }

        [Required(ErrorMessage ="Select Months to Calculate Total Price")]
        public decimal Total { get; set; }
    }
}

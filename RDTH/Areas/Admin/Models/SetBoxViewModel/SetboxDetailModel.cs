using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Areas.Admin.Models.SetBoxViewModel
{
    public class SetboxDetailModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specification { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

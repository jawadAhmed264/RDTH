using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Areas.Admin.Models.SetBoxViewModel
{
    public class SetboxDetailModel
    {
        [Required]
        public string Name { get; set; }
        public string Specification { get; set; }
        public decimal Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RDTH.Areas.Admin.Models.PackageViewModel
{
    public class PackageDetailModel
    {
        [Required]
        public string PackageName { get; set; }
        [Required]
        public int NoOfChannels { get; set; }
        public int NewsChannel { get; set; }
        public int EntertainmentChannel { get; set; }
        public int SportsChannel { get; set; }
        public int DocumentariesChannel { get; set; }
        [Required]
        public int Charges { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

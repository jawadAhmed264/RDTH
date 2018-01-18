using System.ComponentModel.DataAnnotations;

namespace RDTH.Areas.Admin.Models.AdminViewModel
{
    public class AddAdminModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

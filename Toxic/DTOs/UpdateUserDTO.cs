using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class UpdateUserDTO
    {

        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "About is required")]
        public string? About { get; set; }
    }
}

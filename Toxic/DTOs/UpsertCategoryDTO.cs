using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class UpsertCategoryDTO
    {
        [Required(ErrorMessage = "An image is Required.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "A title is Required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "A description is Required.")]
        public string? Description { get; set; }

    }
}

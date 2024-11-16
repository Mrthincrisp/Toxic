using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class UpdateMessageDTO
    {
        [Required(ErrorMessage ="Content is Required.")]
        public string? Content { get; set; }

    }
}

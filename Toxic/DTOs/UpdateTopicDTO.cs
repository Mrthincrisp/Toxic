using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class UpdateTopicDTO
    {
        [Required(ErrorMessage = "A Header is Required.")]
        public string? Header { get; set; }

        [Required(ErrorMessage = "Content is Required.")]
        public string? Content { get; set; }

    }
}

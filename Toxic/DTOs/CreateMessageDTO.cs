using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class CreateMessageDTO
    {
        [Required(ErrorMessage = "Content is Required.")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "CreatedAt is Required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "UserId is Required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ChatId is Required.")]
        public int ChatId { get; set; }
    }
}

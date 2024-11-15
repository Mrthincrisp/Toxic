using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class CreateCommentDTO
    {
        [Required(ErrorMessage ="Content is Required.") ]
        public string? Content { get; set; }

        [Required(ErrorMessage = "UserId is Required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "TopicId is Required.")]
        public int TopicId { get; set; }
    }
}

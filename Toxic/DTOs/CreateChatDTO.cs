using System.ComponentModel.DataAnnotations;

namespace Toxic.DTOs
{
    public class CreateChatDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserNames { get; set; }
    }
}

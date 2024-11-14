namespace Toxic.DTOs
{
    public class CreateTopicDTO
    {
        public string? Header { get; set; }
        public string? Content { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}

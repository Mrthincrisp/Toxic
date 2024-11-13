namespace Toxic.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        public User? User { get; set; }
    }
}

namespace Toxic.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? Content { get; set; }
        public int CommentId { get; set; }
        public int CategoryId { get; set; }
        public int UserId {  get; set; }
        public User? User { get; set; }
        public Category? Categories { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

namespace Toxic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Uid { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Image {  get; set; }
        public string? About { get; set; }
        public bool Admin { get; set; }
        public List<UserChat>? UserChats { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Topic>? Topic { get; set; }
        public List<Message>? Messages { get; set; }
    }
}

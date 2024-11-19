using System.Text.Json.Serialization;
using Toxic.Services;

namespace Toxic.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public List<UserChat>? UserChats { get; set; }
        public List<Message>? Messages { get; set; }

    }
}
using Toxic.Models;

namespace Toxic.Data
{
    public class MessageData
    {
        public static List<Message> Messages =
        [
            new() {Id= 1, Content="first comment", CreatedAt = new DateTime(2024, 11, 13), UserId = 1, ChatId = 1},
            new() {Id = 2, Content = "second comment", CreatedAt = new DateTime(2024, 11, 12), UserId = 2, ChatId = 1},
            new() {Id = 3, Content = "third comment", CreatedAt = new DateTime(2024, 11, 11), UserId = 1, ChatId = 2},
            new() {Id = 4, Content = "fourth comment", CreatedAt = new DateTime(2024, 11, 10), UserId = 2, ChatId = 2},
            new() {Id = 5, Content = "fifth comment", CreatedAt = new DateTime(2024, 11, 09), UserId = 1, ChatId = 3},
            new() {Id = 6, Content = "sixth comment", CreatedAt = new DateTime(2024, 11, 08), UserId = 2, ChatId = 3}
        ];
    }
}

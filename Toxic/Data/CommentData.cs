using Toxic.Models;
namespace Toxic.Data
{
    public class CommentData
    {
        public static List<Comment> Comments =
            [
            new() {Id= 1, Content="first comment", UserId =1, TopicId = 1},
            new() {Id = 2, Content = "second comment", UserId = 2, TopicId = 1},
            new() {Id = 3, Content = "third comment", UserId = 1, TopicId = 2},
            new() {Id = 4, Content = "fourth comment", UserId = 2, TopicId = 2},
            new() {Id = 5, Content = "fifth comment", UserId = 1, TopicId = 3},
            new() {Id = 6, Content = "sixth comment", UserId = 2, TopicId = 3},
            new() {Id = 7, Content = "seventh comment", UserId = 1, TopicId = 4},
            new() {Id = 8, Content = "eighth comment", UserId = 2, TopicId = 4},
            new() {Id = 9, Content = "ninth comment", UserId = 1, TopicId = 5},
            new() {Id = 10, Content = "tenth comment", UserId = 2, TopicId = 5}
            ];
    }
}

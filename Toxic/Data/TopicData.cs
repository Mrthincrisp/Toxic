using Toxic.Models;
namespace Toxic.Data
{
    public class TopicData
    {
        public static List<Topic> Topics =
            [
            new() {Id = 1, Header = "Movie Topic one", Content = "What I think about this movie", CategoryId = 1, UserId =1},
            new() {Id = 2, Header = "Movie Topic Two", Content = "Thoughts on another great film", CategoryId = 2, UserId =1},
            new() {Id = 3, Header = "Book Topic One", Content = "Review of a thrilling novel", CategoryId = 3, UserId =1},
            new() {Id = 4, Header = "Music Topic One", Content = "Discussion on a new album release", CategoryId = 4, UserId =2},
            new() {Id = 5, Header = "Gaming Topic One", Content = "Opinions on the latest RPG", CategoryId = 5, UserId =2}
            ];
    }
}
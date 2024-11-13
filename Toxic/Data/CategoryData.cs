using Toxic.Models;
namespace Toxic.Data
{
    public class CategoryData
    {
        public static List<Category> Categories =
            [
            new() {Id = 1, Image = "URL", Title = "Movies", Description ="Discussions about Movies"},
            new() {Id = 2, Image = "URL", Title = "Music", Description = "Discussions about Music"},
            new() {Id = 3, Image = "URL", Title = "Gaming", Description = "Discussions about Video Games"},
            new() {Id = 4, Image = "URL", Title = "Books", Description = "Discussions about Books and Literature"},
            new() {Id = 5, Image = "URL", Title = "Technology", Description = "Discussions about Tech and Gadgets"}
            ];
    }
}

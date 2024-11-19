using Toxic.Models;

namespace Toxic.Data
{
    public class UserData
    {
        public static List<User> Users =
        [
             new() { Id = 1,
               Uid = "firebaseidwillgohere123",
               UserName = "Derek",
               Email = "Mrthincrisp@gmail.com",
               Image = "image.url",
               About = "Loves exploring new gadgets and technologies.", 
               Admin = true,
             },

             new() { Id = 2,
               Uid = "anotherfirebaseIdwouldgohere456",
               UserName = "Not Derek",
               Email = "Not_Derek@gmail.com",
               Image = "image.url",
               About = "Avid music lover with a passion for discovering new artists.",
               Admin = false
             },

             new() { Id = 3,
               Uid = "fuckyou",
               UserName = "Pissoff",
               Email = "getSmoked@gmail.com",
               Image = "image.url",
               About = "ur mom",
               Admin = false,
             }

        ];
    }
}

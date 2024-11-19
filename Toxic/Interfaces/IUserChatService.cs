using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IUserChatService
    {
        Task<UserChat> AddUserToAChatAsync(int userId, int chatId);
        Task<UserChat> DeleteChatAsync(int userId, int chatId);
    }
}

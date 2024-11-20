using AutoMapper;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChatWithUsersAsync(IMapper mapper, int id, List<int> userIds);
        Task<Chat> GetChatByIdAsync(int id);
        Task<List<Chat>> GetUserChats(int userId);

    }
}

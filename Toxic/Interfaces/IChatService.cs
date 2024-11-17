using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChatWithUsersAsync(IMapper mapper, int id, List<int> userIds);

    }
}

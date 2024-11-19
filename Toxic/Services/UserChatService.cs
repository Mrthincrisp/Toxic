using Microsoft.EntityFrameworkCore;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class UserChatService : IUserChatService
    {
        private readonly IUserChatRepository _userChatRepository;

        public UserChatService(IUserChatRepository userChatRepository)
        {
            _userChatRepository = userChatRepository;
        }

        public async Task<UserChat> AddUserToAChatAsync(int userId, int chatId)
        {
            return await _userChatRepository.AddUserToAChatAsync(userId, chatId);
        }

        public async Task<UserChat> DeleteChatAsync(int userId, int chatId)
        {
            return await _userChatRepository.DeleteChatAsync(userId, chatId);
        }
    }
}

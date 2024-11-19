using AutoMapper;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chat> CreateChatWithUsersAsync(IMapper mapper, int id, List<int> userIds)
        {
            return await _chatRepository.CreateChatWithUsersAsync(mapper, id, userIds);
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _chatRepository.GetChatByIdAsync(id);
        }

        public async Task<List<Chat>> GetUserChats(int id)
        {
            return await _chatRepository.GetUserChats(id);
        }
    }
}

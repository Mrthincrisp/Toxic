using AutoMapper;
using Toxic.DTOs;
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
    }
}

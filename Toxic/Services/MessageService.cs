using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;

        public MessageService(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public async Task<Message> CreateAMessage(IMapper mapper, CreateMessageDTO createDTO)
        {
            return await CreateAMessage(mapper, createDTO);
        }

        public async Task<Message> DeleteMessageAsync(int id)
        {
            return await DeleteMessageAsync(id);
        }

        public async Task<List<Message>> GetMessagesInAChatAsync(int id)
        {
            return await GetMessagesInAChatAsync(id);
        }

        public Task<Message> GetSingleMessageAsync(int id)
        {
            return GetSingleMessageAsync(id);
        }

        public Task<Message> UpdateMessageAsync(int id, IMapper mapper, UpdateMessageDTO updateDTO)
        {
            return UpdateMessageAsync(id, mapper, updateDTO);
        }
    }
}

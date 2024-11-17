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
            return await _messageRepo.CreateAMessage(mapper, createDTO);
        }

        public async Task<Message> DeleteMessageAsync(int id)
        {
            return await _messageRepo.DeleteMessageAsync(id);
        }

        public async Task<List<Message>> GetMessagesInAChatAsync(int id)
        {
            return await _messageRepo.GetMessagesInAChatAsync(id);
        }

        public async Task<Message> GetSingleMessageAsync(int id)
        {
            return await _messageRepo.GetSingleMessageAsync(id);
        }

        public async Task<Message> UpdateMessageAsync(int id, IMapper mapper, UpdateMessageDTO updateDTO)
        {
            return await _messageRepo.UpdateMessageAsync(id, mapper, updateDTO);
        }
    }
}

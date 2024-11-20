using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ToxicDbContext _context;

        public MessageRepository(ToxicDbContext context)
        {
            _context = context;
        }

        //Create a Message
        public async Task<Message> CreateAMessage(IMapper mapper, CreateMessageDTO createDTO)
        {
            var message = mapper.Map<Message>(createDTO);

            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return message;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        //Delete a message
        public async Task<Message> DeleteMessageAsync(int id)
        {
            var deletedMessage = await _context.Messages.FindAsync(id);

            _context.Messages.Remove(deletedMessage);
            await _context.SaveChangesAsync();
            return deletedMessage;
        }

        //Get chat messages
        public async Task<List<Message>> GetMessagesInAChatAsync(int id)
        {
            return await _context.Messages
                .Where(m => m.ChatId == id)
                .ToListAsync();

        }

        //Get single message
        public async Task<Message> GetSingleMessageAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        //Update a message
        public async Task<Message> UpdateMessageAsync(int id, IMapper mapper, UpdateMessageDTO updateDTO)
        {
            var updatedMessage = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);

            mapper.Map(updateDTO, updatedMessage);

            try
            {
                await _context.SaveChangesAsync();
                return updatedMessage;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}

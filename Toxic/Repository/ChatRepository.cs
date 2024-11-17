using AutoMapper;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Toxic.Repository
{
    public class ChatRepository : IChatRepository
    {
        public readonly ToxicDbContext _context;

        public ChatRepository(ToxicDbContext context)
        {
            _context = context;
        }

        //Create a Chat with users
        public async Task<Chat> CreateChatWithUsersAsync(IMapper mapper, int id, List<int> userIds)
        {
            userIds.Add(id);

            var users = await _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();

            var createdChatDTO = new CreateChatDTO
            {
                Name = $"{string.Join(", ", users.Select(u => u.UserName))}"
            };

            var createdChat = mapper.Map<Chat>(createdChatDTO);

            _context.Chats.Add(createdChat);
            await _context.SaveChangesAsync();

            var userChats = users.Select(u => new UserChat
            {
                UserId = u.Id,
                ChatId = createdChat.Id 
            }).ToList();

            _context.UserChats.AddRange(userChats);
            await _context.SaveChangesAsync();

            return createdChat;
        }

        // Grab all messages in a chat.

        //Delete a chat
    }
}

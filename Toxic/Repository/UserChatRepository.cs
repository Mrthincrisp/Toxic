using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Repository
{
    public class UserChatRepository : IUserChatRepository
    {
        public readonly ToxicDbContext _context;
        public readonly ILogger<UserChatRepository> _logger;

        public UserChatRepository(ToxicDbContext context, ILogger<UserChatRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add a user to chat
        public async Task<UserChat> AddUserToAChatAsync(int userId, int chatId)
        {

                var userChat = new UserChat
                {
                    ChatId = chatId,
                    UserId = userId
                };

                _context.UserChats.Add(userChat);
                await _context.SaveChangesAsync();
                return userChat;
            
        }

        //Delete a userchat
        public async Task<UserChat> DeleteChatAsync(int userId, int chatId)
        {
            var userChat = await _context.UserChats
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == chatId);

            if (userChat == null)
            {
                return null;
            }

            _context.UserChats.Remove(userChat);
            await _context.SaveChangesAsync();

            var userCount = await _context.UserChats
                .Where(uc => uc.ChatId == chatId)
                .CountAsync();

            if (userCount == 0)
            {
                var messages = _context.Messages.Where(m => m.ChatId == chatId);
                _context.Messages.RemoveRange(messages);

                var chat = await _context.Chats.FindAsync(chatId);
                if (chat != null)
                {
                    _context.Chats.Remove(chat);
                }
                await _context.SaveChangesAsync();
            }
            return userChat;
        }

    }
}

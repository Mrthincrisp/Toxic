﻿using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> CreateChatWithUsersAsync(IMapper mapper, int id, List<int> userIds);
        Task<Chat> GetChatByIdAsync(int id);
        Task<List<Chat>> GetUserChats(int userId);
    }
}

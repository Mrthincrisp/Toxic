using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepo)
        {
            _repository = userRepo;
        }

        public async Task<User> UserCheckAsync(int id)
        {
            return await _repository.UserCheckAsync(id);
        }

        public async Task<User> RegisterUserAsync(IMapper mapper, CreateUserDTO createDTO)
        {
            return await _repository.RegisterUserAsync(mapper, createDTO);
        }

        public async Task<User> UserLoginAsync(string uid)
        {
            return await _repository.UserLoginAsync(uid);
        }

        public async Task<User> GetASingleUserAsync(int id)
        {
            return await _repository.GetASingleUserAsync(id);
        }

        public async Task<User> UpdateASingleUserAsync(int id, IMapper mapper, UpdateUserDTO updateDTO)
        {
            return await _repository.UpdateASingleUserAsync(id, mapper, updateDTO);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            return await _repository.DeleteUserAsync(id);
        }
    }
}

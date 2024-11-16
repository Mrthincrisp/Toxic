using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IUserService
    {
        Task<User> UserCheckAsync(int id);
        Task<User> RegisterUserAsync(IMapper mapper, CreateUserDTO createDTO);
        Task<User> UserLoginAsync(string uid);
        Task<User> GetASingleUserAsync(int id);
        Task<User> UpdateASingleUserAsync(int id, IMapper mapper, UpdateUserDTO updateDTO);
        Task<User> DeleteUserAsync(int id);
    }
}

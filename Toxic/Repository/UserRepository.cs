using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ToxicDbContext _context;

        public UserRepository(ToxicDbContext context)
        {
            _context = context;
        }

        // Delete a user
        public async Task<User> DeleteUserAsync(int id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return userToDelete;
        }

        // Get a single user
        public async Task<User> GetASingleUserAsync(int id)
        {
            var singleUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            return singleUser;
        }

        // Register User
        public async Task<User> RegisterUserAsync(IMapper mapper, CreateUserDTO createDTO)
        {
            var createdUser = mapper.Map<User>(createDTO);

            try
            {
                _context.Users.Add(createdUser);
                await _context.SaveChangesAsync();
                return createdUser;
            }
            catch (DbUpdateException)
            {
                return null;
            }

        }

        // Update user
        public async Task<User> UpdateASingleUserAsync(int id, IMapper mapper, UpdateUserDTO updateDTO)
        {
            var userUpdated = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            mapper.Map(userUpdated, updateDTO);
            try
            {
                await _context.SaveChangesAsync();
                return userUpdated;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        // Check a user
        public async Task<User> UserCheckAsync(int id)
        {
            var userChecked = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
          
            return userChecked;
        }

        // User login
        public async Task<User> UserLoginAsync(string uid)
        {
            var authUser = await  _context.Users.Where(u => u.Uid == uid).FirstOrDefaultAsync();

            return authUser;
        }
    }
}

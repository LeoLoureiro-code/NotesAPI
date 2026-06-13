using Microsoft.EntityFrameworkCore;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Interfaces;
using NotesAPI.EF.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.EF.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        private readonly NotesAPIDbContext _context;

        public UserRepository(NotesAPIDbContext context) {
        
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }
            _context.Users.Remove(existingUser); 
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User not found.");
            }
            existingUser.Email = user.Email;
            existingUser.HashPassword = user.HashPassword;
            existingUser.Role = user.Role;

            return existingUser;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync(); 
        }


    }
}

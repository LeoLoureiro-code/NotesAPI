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

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

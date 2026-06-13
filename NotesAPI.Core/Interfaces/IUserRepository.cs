using NotesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(int id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);

        Task<int> SaveChangesAsync();
    }
}

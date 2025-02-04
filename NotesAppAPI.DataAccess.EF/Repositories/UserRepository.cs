using Microsoft.AspNetCore.Identity;
using NotesAppAPI.DataAccess.EF.Context;
using NotesAppAPI.DataAccess.EF.Models;
using System.Linq;

namespace NotesAppAPI.DataAccess.EF.Repositories
{
    public class UserRepository
    {
        private readonly NotesAPIDbContext _context;

        public UserRepository(NotesAPIDbContext context)
        {
            _context = context;
        }

        public int CreateUser(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.UserPassword = passwordHasher.HashPassword(user, password);  
            _context.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }


        public int UpdatePassword(int userId, string password)
        {
            User existingUser = _context.Users.Find(userId);
            if (existingUser == null)
            {
               
                return -1; 
            }

            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(existingUser, password);

            existingUser.UserPassword = hashedPassword;
            _context.SaveChanges();

            return userId;
        }

        public bool DeleteUser(int userId)
        {
            User existingUser = _context.Users.Find(userId);
            if (existingUser == null)
            {
                // Handle user not found
                return false;
            }

            _context.Remove(existingUser);
            _context.SaveChanges();
            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.UserEmail == email);
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            return _context.Users
                .FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public bool UpdateRefreshToken(int userId, string refreshToken)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser != null)
            {
                existingUser.RefreshToken = refreshToken;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

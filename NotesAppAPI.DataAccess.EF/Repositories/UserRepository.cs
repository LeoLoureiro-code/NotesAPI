using Microsoft.AspNetCore.Identity;
using NotesAppAPI.DataAccess.EF.Context;
using NotesAppAPI.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppAPI.DataAccess.EF.Repositories
{
    public class UserRepository
    {
        private readonly NotesAPIDbContext _context;

        public UserRepository(NotesAPIDbContext context)
        {
            _context = context;
        }

        public int CreateUser(User user)
        {
            
            _context.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public int UpdatePassword(int userId, string password)
        {
           User existingUser = _context.Users.Find(userId);
           var passwordHasher = new PasswordHasher<object>();

            /*hashed password for security*/
            string hashedPassword = passwordHasher.HashPassword(null, password);

           existingUser.UserPassword = hashedPassword;
            _context.SaveChanges();

            return userId;
        }

        public bool DeleteUser(int userId)
        {
            User existingUser = _context.Users.Find(userId);
            _context.Remove(existingUser);
            _context.SaveChanges();
            return true;
        }

        public User GetUserById(int userId)
        {
            User user = _context.Users.Find(userId);
            return user;
        }
    }
}

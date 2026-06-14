using Microsoft.AspNetCore.Identity;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.EF.Data.Services
{
    public class PasswordHasherService: IPasswordHasher
    {

        private readonly PasswordHasher<User> _passwordHasher = new();
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new User(), password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(
            new User(),
            hashedPassword,
            providedPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}

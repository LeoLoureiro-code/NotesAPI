using NotesAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string HashPassword { get; set; } = null!;

        public Role Role { get; set; }

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}

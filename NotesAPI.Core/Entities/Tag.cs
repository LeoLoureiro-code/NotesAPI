using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Note> Notes { get; set; }
            = new List<Note>();
    }
}

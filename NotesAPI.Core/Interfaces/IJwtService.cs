using NotesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}

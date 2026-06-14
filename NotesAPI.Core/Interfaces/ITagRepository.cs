using NotesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetTagsByUserAsync(int userId);

        Task<Tag?> GetTagByIdAsync(int id);

        Task<Tag?> GetTagByNameAsync(
            int userId,
            string name);

        Task AddTagAsync(Tag tag);

        Task UpdateTagAsync(Tag tag);

        Task DeleteTagAsync(int id, int userId);

        Task<int> SaveChangesAsync();
    }
}

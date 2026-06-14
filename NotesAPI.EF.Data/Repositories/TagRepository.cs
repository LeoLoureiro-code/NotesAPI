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
    public class TagRepository : ITagRepository
    {

        private readonly NotesAPIDbContext _context;

        public TagRepository(NotesAPIDbContext context)
        {
            _context = context;
        }


        public async Task AddTagAsync(Tag tag)
        {
            ArgumentNullException.ThrowIfNull(tag);
            await _context.Tags.AddAsync(tag);
        }

        public async Task DeleteTagAsync(int tagId, int userId)
        {
            var existingTag = await GetTagByIdAsync(tagId);

            if (existingTag != null && existingTag.UserId == userId)
            {
                _context.Tags.Remove(existingTag);
            }
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagId == id);
        }

        public async Task<Tag?> GetTagByNameAsync(int userId, string name)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(name);


            return await _context.Tags
                .FirstOrDefaultAsync(t =>
                    t.UserId == userId &&
                    t.Name == name);
        }

        public async Task<IEnumerable<Tag>> GetTagsByUserAsync(int userId)
        {
            return await _context.Tags
                 .Where(t => t.UserId == userId)
                 .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            ArgumentNullException.ThrowIfNull(tag);

            var existingTag = await _context.Tags
                .FirstOrDefaultAsync(t =>
                    t.TagId == tag.TagId &&
                    t.UserId == tag.UserId);

            if (existingTag is null)
                return;

            existingTag.Name = tag.Name;
        }
    }
}

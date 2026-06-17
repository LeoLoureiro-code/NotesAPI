using Microsoft.EntityFrameworkCore;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Interfaces;
using NotesAPI.EF.Data.Context;

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

        public async Task DeleteTagAsync(
            int tagId,
            int userId)
        {
            var existingTag = await GetTagByIdAsync(
                tagId,
                userId);

            if (existingTag != null)
            {
                _context.Tags.Remove(existingTag);
            }
        }

        public async Task<Tag?> GetTagByIdAsync(
            int tagId,
            int userId)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t =>
                    t.TagId == tagId &&
                    t.UserId == userId);
        }

        public async Task<Tag?> GetTagByNameAsync(
            int userId,
            string name)
        {
            ArgumentException
                .ThrowIfNullOrWhiteSpace(name);

            return await _context.Tags
                .FirstOrDefaultAsync(t =>
                    t.UserId == userId &&
                    t.Name == name);
        }

        public async Task<IEnumerable<Tag>>
            GetTagsByUserAsync(int userId)
        {
            return await _context.Tags
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            ArgumentNullException.ThrowIfNull(tag);

            var existingTag =
                await GetTagByIdAsync(
                    tag.TagId,
                    tag.UserId);

            if (existingTag == null)
            {
                return;
            }

            existingTag.Name = tag.Name;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
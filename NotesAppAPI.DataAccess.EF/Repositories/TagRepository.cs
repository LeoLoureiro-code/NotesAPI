using NotesAppAPI.DataAccess.EF.Context;
using NotesAppAPI.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppAPI.DataAccess.EF.Repositories
{
    public class TagRepository
    {
        private readonly NotesAPIDbContext _context;

        public TagRepository(NotesAPIDbContext context)
        {
            _context = context;
        }

        public int CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();

            return tag.TagId;
        }

        public int UpdateTag (int tagId, string tagName)
        {
            Tag existingTag = _context.Tags.Find(tagId);

            existingTag.TagName = tagName;

            _context.SaveChanges();
            return existingTag.TagId;
        }

        public int DeleteTag (int tagId)
        {
            Tag existingTag = _context.Tags.Find (tagId);

            _context.Remove(existingTag);
            _context.SaveChanges();

            return 0;
        }

        public List<Tag> GetAllTags()
        {
            List<Tag> tags = _context.Tags.ToList();
            return tags;
        }

        public Tag GetTagById(int tagId) 
        {
            Tag tag = _context.Tags.Find(tagId);
            return tag;
        }
    }
}

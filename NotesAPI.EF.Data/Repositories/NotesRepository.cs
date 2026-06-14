using Microsoft.EntityFrameworkCore;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Interfaces;
using NotesAPI.EF.Data.Context;

namespace NotesAPI.EF.Data.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly NotesAPIDbContext _context;

        public NotesRepository(NotesAPIDbContext context)
        {
            _context = context;
        }

        public async Task CreateNoteAsync(Note note)
        {
            ArgumentNullException.ThrowIfNull(note);

            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.Notes.AddAsync(note);
        }

        public async Task DeleteNoteAsync(int noteId, int userId)
        {
            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);

            if (existingNote == null)
            {
                throw new KeyNotFoundException(
                    $"Note with id {noteId} not found.");
            }

            _context.Notes.Remove(existingNote);
        }

        public async Task<IEnumerable<Note>> GetArchivedNotesAsync(int userId)
        {
            return await _context.Notes
                .Where(n =>
                    n.UserId == userId &&
                    n.IsArchived)
                .ToListAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(int noteId, int userId)
        {
            return await _context.Notes
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);
        }

        public async Task<IEnumerable<Note>> GetNotesByTagAsync(
            int userId,
            int tagId)
        {
            return await _context.Notes
                .Where(n =>
                    n.UserId == userId &&
                    n.Tags.Any(t => t.TagId == tagId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByUserAsync(int userId)
        {
            return await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> SearchNotesAsync(
            int userId,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<Note>();
            }

            return await _context.Notes
                .Where(n =>
                    n.UserId == userId &&
                    (n.Title.Contains(searchTerm) ||
                     n.Content.Contains(searchTerm)))
                .ToListAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            ArgumentNullException.ThrowIfNull(note);

            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == note.NoteId &&
                    n.UserId == note.UserId);

            if (existingNote == null)
            {
                throw new KeyNotFoundException(
                    $"Note with id {note.NoteId} not found.");
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.IsArchived = note.IsArchived;
            existingNote.UpdatedAt = DateTime.UtcNow;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
using NotesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.Core.Interfaces
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetNotesByUserAsync(int userId);
        Task<Note?> GetNoteByIdAsync(int noteId, int userId);
        Task<IEnumerable<Note>> SearchNotesAsync(
            int userId,
            string searchTerm);
        Task<IEnumerable<Note>> GetArchivedNotesAsync(
            int userId);
        Task<IEnumerable<Note>> GetNotesByTagAsync(
            int userId,
            int tagId);
        Task CreateNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
        Task SaveChangesAsync();
    }
}

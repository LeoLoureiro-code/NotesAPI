using NotesAppAPI.DataAccess.EF.Context;
using NotesAppAPI.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppAPI.DataAccess.EF.Repositories
{
    public class NoteRepository
    {
        private readonly NotesAPIDbContext _context;


        public NoteRepository(NotesAPIDbContext context)
        {
            _context = context;
        }

        public int CreateNote(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();

            return note.NoteId;
        }

        public int Update(int noteId, string noteTitle, string noteContent)
        {
            Note existingNote = _context.Notes.Find(noteId);

            existingNote.NoteTitle = noteTitle;
            existingNote.NoteContent = noteContent;

            _context.SaveChanges();
            return existingNote.NoteId;
        }


        public bool Delete(int noteId)
        {
            Note existingNote = _context.Notes.Find(noteId);
            _context.Remove(existingNote);
            _context.SaveChanges();
            return true;
        }

        public List<Note> GetAllNotes()
        {
            List<Note> notes = _context.Notes.ToList();
            return notes;
        }

        public Note GetNoteById(int noteId)
        {
            Note note = _context.Notes.Find(noteId);
            return note;
        }
    }
}

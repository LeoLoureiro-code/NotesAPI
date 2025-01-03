using System;
using System.Collections.Generic;

namespace NotesAppAPI.DataAccess.EF.Models;

public class Note
{

    public Note()
    {

    }

    public Note(string noteTitle, string noteContent, int userId)
    {
        NoteTitle = noteTitle;
        NoteContent = noteContent;
        UserId = userId;
    }

    public int NoteId { get; set; }

    public string NoteTitle { get; set; } = null!;

    public string NoteContent { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();

    public virtual User? User { get; set; }
}

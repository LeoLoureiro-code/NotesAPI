using System;
using System.Collections.Generic;

namespace NotesAppAPI.DataAccess.EF.Models;

public class User
{

    public User()
    {

    }

    public User( string userEmail, string userPassword, ICollection<Note> notes = null, ICollection<NotesTag> notesTags= null)
    {
        UserEmail = userEmail;
        UserPassword = userPassword;
        Notes = notes;
        NotesTags = notesTags;
    }

    public int UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();
}

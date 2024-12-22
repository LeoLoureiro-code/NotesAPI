using System;
using System.Collections.Generic;

namespace NotesApp.DataAcccess.EF.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();
}

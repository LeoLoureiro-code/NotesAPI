using System;
using System.Collections.Generic;

namespace NotesApp.DataAcccess.EF.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public string NoteTitle { get; set; } = null!;

    public string NoteContent { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();

    public virtual User? User { get; set; }
}

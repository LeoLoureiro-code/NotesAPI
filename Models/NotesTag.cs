using System;
using System.Collections.Generic;

namespace NotesAPI.Models;

public partial class NotesTag
{
    public int NotesNoteId { get; set; }

    public int NotesUserId { get; set; }

    public int TagsTagId { get; set; }

    public virtual Note NotesNote { get; set; } = null!;

    public virtual User NotesUser { get; set; } = null!;

    public virtual Tag TagsTag { get; set; } = null!;
}

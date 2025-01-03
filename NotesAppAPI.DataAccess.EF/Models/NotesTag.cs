using System;
using System.Collections.Generic;

namespace NotesAppAPI.DataAccess.EF.Models;

public class NotesTag
{

    public NotesTag() 
    { 
    
    }


    public NotesTag(int notesNoteId, int notesUserId, int tagsTagId, Note notesNote, User notesUser, Tag tagsTag)
    {
        NotesNoteId = notesNoteId;
        NotesUserId = notesUserId;
        TagsTagId = tagsTagId;
        NotesNote = notesNote;
        NotesUser = notesUser;
        TagsTag = tagsTag;
    }

    public int NotesNoteId { get; set; }

    public int NotesUserId { get; set; }

    public int TagsTagId { get; set; }

    public virtual Note NotesNote { get; set; } = null!;

    public virtual User NotesUser { get; set; } = null!;

    public virtual Tag TagsTag { get; set; } = null!;
}

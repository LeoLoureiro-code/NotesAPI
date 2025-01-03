using System;
using System.Collections.Generic;

namespace NotesAppAPI.DataAccess.EF.Models;

public class Tag
{

    public Tag()
    {

    }

    public Tag(int tagId, string tagName)
    {
        TagId = tagId;
        TagName = tagName;
    }

    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();
}

﻿using System;
using System.Collections.Generic;

namespace NotesApp.DataAcccess.EF.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();
}

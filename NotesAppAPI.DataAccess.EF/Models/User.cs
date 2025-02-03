using System;
using System.Collections.Generic;

namespace NotesAppAPI.DataAccess.EF.Models;

public class User
{
    public User() { }

    public User(string userEmail, string userPassword)
    {
        UserEmail = userEmail;
        UserPassword = userPassword;
    }

    public int UserId { get; set; }
    public string UserEmail { get; set; } = null!;
    public string UserPassword { get; set; } = null!;

    // ✅ Added Refresh Token properties
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    public virtual ICollection<NotesTag> NotesTags { get; set; } = new List<NotesTag>();
}

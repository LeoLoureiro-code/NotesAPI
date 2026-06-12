using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTO
{
    public class RegisterRequest
    {
        [Required]
        string Email { get; set; } = string.Empty;
        [Required]
        string Password { get; set; } = string.Empty;

    }
}

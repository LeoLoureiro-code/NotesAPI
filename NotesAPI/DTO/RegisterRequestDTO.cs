using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}

namespace NotesAPI.DTO
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}

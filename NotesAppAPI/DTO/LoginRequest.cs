namespace NotesAppAPI.DTO
{
    public class LoginRequest
    {
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }
}

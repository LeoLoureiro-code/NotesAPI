namespace NotesAPI.DTO
{
    public class NotesInformationDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public bool IsArchived { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}

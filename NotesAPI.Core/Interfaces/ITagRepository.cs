using NotesAPI.Core.Entities;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetTagsByUserAsync(int userId);

    Task<Tag?> GetTagByIdAsync(int tagId, int userId);

    Task<Tag?> GetTagByNameAsync(int userId, string name);

    Task AddTagAsync(Tag tag);

    Task UpdateTagAsync(Tag tag);

    Task DeleteTagAsync(int tagId, int userId);

    Task<int> SaveChangesAsync();
}
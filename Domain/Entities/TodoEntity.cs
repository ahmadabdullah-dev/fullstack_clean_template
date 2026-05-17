namespace Domain.Entities;
public class TodoEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; }
    public bool IsCompleted { get; set; } 
    public DateTimeOffset CreatedDate { get; set; }  

    public string? AppUserId { get; set; }
    public AppUserEntity? AppUser { get; set; }
}

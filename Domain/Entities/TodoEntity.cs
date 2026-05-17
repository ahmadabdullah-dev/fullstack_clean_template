namespace Domain.Entities;
public class TodoEntity
{
    public Guid Id { get; set; } 
    public string? Title { get; set; }
    public bool IsCompleted { get; set; } 
    public DateTimeOffset CreatedDate { get; set; } 

    public string? AppUserId { get; set; }
    public AppUserEntity? AppUser { get; set; }
}

namespace Domain.Entities;
public class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}

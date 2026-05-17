namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetUserId();
    Task<AppUser> GetUserAsync();
}

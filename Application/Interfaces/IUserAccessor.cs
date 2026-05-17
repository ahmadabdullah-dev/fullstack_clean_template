namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetUserId();
    Task<AppUserEntity> GetUserAsync();
}

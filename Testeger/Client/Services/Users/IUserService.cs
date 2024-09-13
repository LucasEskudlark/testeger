namespace Testeger.Client.Services.Users;

public interface IUserService
{
    Task<string> GetUserNameAsync();
}

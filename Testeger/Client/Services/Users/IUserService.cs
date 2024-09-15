using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.Services.Users;

public interface IUserService
{
    Task<string> GetUserNameAsync();
    Task<IEnumerable<UserByRoleViewModel>> GetUsersByRoleAsync(string projectId, string roleName);
}

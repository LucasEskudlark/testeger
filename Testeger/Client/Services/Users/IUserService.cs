using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.Services.Users;

public interface IUserService
{
    Task<string> GetUserNameAsync();
    Task<IEnumerable<UserNameIdViewModel>> GetUsersByRoleAsync(string projectId, string roleName);
    Task<UserViewModel> GetUserByIdAsync(string userId);
    Task<IEnumerable<UserViewModel>> GetUsersByProjectIdAsync(string projectId);
    Task<string> GetUserIdAsync();
}

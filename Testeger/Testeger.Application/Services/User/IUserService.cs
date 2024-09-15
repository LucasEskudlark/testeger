using Testeger.Shared.DTOs.Common;

namespace Testeger.Application.Services.User;

public interface IUserService
{
    Task<IEnumerable<UserByRoleDto>> GetUsersByProjectRoleAsync(string roleName);
}

using Testeger.Shared.DTOs.Common;

namespace Testeger.Application.Services.User;

public interface IUserService
{
    Task<IEnumerable<UserNameIdDto>> GetUsersByProjectRoleAsync(string roleName);
    Task<UserNameIdDto> GetUserByIdAsync(string userId);
}

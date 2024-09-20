using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Responses.User;

namespace Testeger.Application.Services.User;

public interface IUserService
{
    Task<IEnumerable<UserNameIdDto>> GetUsersByProjectRoleAsync(string roleName);
    Task<UserNameIdDto> GetUserByIdAsync(string userId);
    Task<IEnumerable<GetUserResponse>> GetUsersByProject(string projectId);
}

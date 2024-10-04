using Microsoft.AspNetCore.Identity;

namespace Testeger.Application.Services.Role;

public interface IRoleService
{
    Task<List<IdentityRole>> GetAllProjectRolesAsync(string projectId);
    Task RemoveAllUsersFromRoleAsync(string roleName);
    Task RemoveRoleFromUserAsync(string roleName, string userId);
    Task DeleteRoleAsync(string roleName);
}

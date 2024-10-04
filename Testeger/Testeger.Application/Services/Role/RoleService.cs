using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Testeger.Domain.Models.Entities;

namespace Testeger.Application.Services.Role;

public class RoleService : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private async Task<IdentityRole> GetRoleOrThrowAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName)
            ?? throw new ArgumentException($"Role '{roleName}' not found.", nameof(roleName));

        return role;
    }

    public async Task<List<IdentityRole>> GetAllProjectRolesAsync(string projectId)
    {
        var roles = await _roleManager.Roles.Where(r => r.Name != null && r.Name.Contains(projectId)).ToListAsync();

        return roles;
    }

    public async Task RemoveAllUsersFromRoleAsync(string roleName)
    {
        _ = await GetRoleOrThrowAsync(roleName);

        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

        if (usersInRole.Count == 0) return;

        foreach (var user in usersInRole)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }

    public async Task RemoveRoleFromUserAsync(string roleName, string userId)
    {
        _ = await GetRoleOrThrowAsync(roleName);
        var user = await _userManager.FindByIdAsync(userId) 
            ?? throw new ArgumentException($"User with id {userId} not found");

        await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task DeleteRoleAsync(string roleName)
    {
        var role = await GetRoleOrThrowAsync(roleName);

        await _roleManager.DeleteAsync(role);
    }
}

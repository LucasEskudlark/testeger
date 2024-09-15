using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Common;

namespace Testeger.Application.Services.User;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<UserByRoleDto>> GetUsersByProjectRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName)
            ?? throw new ArgumentNullException($"Role {roleName} not found");

        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

        var users = _mapper.Map<IEnumerable<UserByRoleDto>>(usersInRole);

        return users;
    }
}

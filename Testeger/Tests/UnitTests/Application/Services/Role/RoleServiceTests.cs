﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Testeger.Application.Services.Role;

namespace UnitTests.Application.Services.Role;

public class RoleServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly RoleService _roleService;

    public RoleServiceTests()
    {
#pragma warning disable CS8625
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
#pragma warning restore CS8625

        _roleService = new RoleService(_mockUserManager.Object, _mockRoleManager.Object);
    }

    [Fact]
    public async Task RemoveAllUsersFromRoleAsync_GivenRoleExists_ShouldRemoveUsers()
    {
        var roleName = "TestRole";
        var users = new List<ApplicationUser>
            {
                new() { Id = "User1" },
                new() { Id = "User2" }
            };

        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(new IdentityRole(roleName));
        _mockUserManager.Setup(um => um.GetUsersInRoleAsync(roleName)).ReturnsAsync(users);
        _mockUserManager.Setup(um => um.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), roleName))
            .ReturnsAsync(IdentityResult.Success);

        await _roleService.RemoveAllUsersFromRoleAsync(roleName);

        _mockUserManager.Verify(um => um.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), roleName), Times.Exactly(2));
    }

    [Fact]
    public async Task RemoveAllUsersFromRoleAsync_GivenRoleDoesNotExist_ShouldThrowArgumentException()
    {
        var roleName = "NonExistentRole";
        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(null as IdentityRole);

        await _roleService.Invoking(rs => rs.RemoveAllUsersFromRoleAsync(roleName))
            .Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Role '{roleName}' not found. (Parameter 'roleName')");
    }

    [Fact]
    public async Task RemoveRoleFromUserAsync_GivenUserAndRoleExist_ShouldRemoveRole()
    {
        var roleName = "TestRole";
        var userId = "TestUser";
        var user = new ApplicationUser { Id = userId };

        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(new IdentityRole(roleName));
        _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

        await _roleService.RemoveRoleFromUserAsync(roleName, userId);

        _mockUserManager.Verify(um => um.RemoveFromRoleAsync(user, roleName), Times.Once);
    }

    [Fact]
    public async Task RemoveRoleFromUserAsync_GivenUserDoesNotExist_ShouldThrowArgumentException()
    {
        var roleName = "TestRole";
        var userId = "NonExistentUser";

        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(new IdentityRole(roleName));
        _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(null as ApplicationUser);

        await _roleService.Invoking(rs => rs.RemoveRoleFromUserAsync(roleName, userId))
            .Should().ThrowAsync<ArgumentException>()
            .WithMessage($"User with id {userId} not found");
    }

    [Fact]
    public async Task DeleteRoleAsync_GivenRoleExists_ShouldDeleteRole()
    {
        var roleName = "TestRole";
        var role = new IdentityRole(roleName);

        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(role);
        _mockRoleManager.Setup(rm => rm.DeleteAsync(role)).ReturnsAsync(IdentityResult.Success);

        await _roleService.DeleteRoleAsync(roleName);

        _mockRoleManager.Verify(rm => rm.DeleteAsync(role), Times.Once);
    }

    [Fact]
    public async Task DeleteRoleAsync_GivenRoleDoesNotExist_ShouldThrowArgumentException()
    {
        var roleName = "NonExistentRole";
        _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(null as IdentityRole);

        await _roleService.Invoking(rs => rs.DeleteRoleAsync(roleName))
            .Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Role '{roleName}' not found. (Parameter 'roleName')");
    }
}
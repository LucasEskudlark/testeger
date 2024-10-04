using Microsoft.AspNetCore.Identity;
using Testeger.Infra.Repositories.Users;
using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Responses.User;

namespace UnitTests.Application.Services.User;

public class UserServiceTests : BaseServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    private readonly Mock<RoleManager<IdentityRole>> _roleManager;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
#pragma warning disable CS8625
        _userManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _roleManager = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
        _userRepository = new Mock<IUserRepository>();
#pragma warning restore CS8625

        _userService = new UserService(
            _unitOfWork.Object,
            _mapper.Object,
            _userManager.Object,
            _roleManager.Object,
            _userRepository.Object
        );
    }

    [Fact]
    public async Task GetUsersByProjectRoleAsync_ShouldReturnUsersRoleExists()
    {
        var roleName = "TestRole";
        var role = new IdentityRole(roleName);
        var users = _fixture.CreateMany<ApplicationUser>(2).ToList();
        var userDtos = _fixture.CreateMany<UserNameIdDto>(2);

        _roleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(role);
        _userManager.Setup(um => um.GetUsersInRoleAsync(roleName)).ReturnsAsync(users);
        _mapper.Setup(m => m.Map<IEnumerable<UserNameIdDto>>(users)).Returns(userDtos);

        var result = await _userService.GetUsersByProjectRoleAsync(roleName);

        result.Should().BeEquivalentTo(userDtos);
        _roleManager.Verify(rm => rm.FindByNameAsync(roleName), Times.Once);
        _userManager.Verify(um => um.GetUsersInRoleAsync(roleName), Times.Once);
        _mapper.Verify(m => m.Map<IEnumerable<UserNameIdDto>>(users), Times.Once);
    }

    [Fact]
    public async Task GetUsersByProjectRoleAsync_GivenRoleDoesNotExist_ShouldThrowArgumentNullException()
    {
        var roleName = "NonExistentRole";
        _roleManager.Setup(rm => rm.FindByNameAsync(roleName)).ReturnsAsync(null as IdentityRole);

        await _userService.Invoking(us => us.GetUsersByProjectRoleAsync(roleName))
            .Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetUserByIdAsync_GivenUserExists_ShouldReturnUser()
    {
        var userId = "TestUserId";
        var user = new ApplicationUser { Id = userId };
        var userDto = _fixture
            .Build<GetUserResponse>()
            .With(dto => dto.Id, userId)
            .Create();

        _userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _mapper.Setup(m => m.Map<GetUserResponse>(user)).Returns(userDto);

        var result = await _userService.GetUserByIdAsync(userId);

        result.Should().BeEquivalentTo(userDto);
        _userManager.Verify(um => um.FindByIdAsync(userId), Times.Once);
        _mapper.Verify(m => m.Map<GetUserResponse>(user), Times.Once);
    }

    [Fact]
    public async Task GetUserByIdAsync_GivenUserDoesNotExist_ShouldThrowArgumentException()
    {
        var userId = "NonExistentUserId";
        _userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(null as ApplicationUser);

        await _userService.Invoking(us => us.GetUserByIdAsync(userId))
            .Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task GetUsersByProject_GivenProjectExists_ShouldReturnUsers()
    {
        var projectId = "TestProjectId";
        var users = _fixture.CreateMany<ApplicationUser>(2).ToList();
        var userDtos = _fixture.CreateMany<GetUserResponse>(2);


        _userRepository.Setup(ur => ur.GetUsersByProject(projectId)).ReturnsAsync(users);
        _mapper.Setup(m => m.Map<IEnumerable<GetUserResponse>>(users)).Returns(userDtos);

        var result = await _userService.GetUsersByProject(projectId);

        result.Should().BeEquivalentTo(userDtos);
        _userRepository.Verify(ur => ur.GetUsersByProject(projectId), Times.Once);
        _mapper.Verify(m => m.Map<IEnumerable<GetUserResponse>>(users), Times.Once);
    }
}

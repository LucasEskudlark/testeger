using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Token;
using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Requests.Authentication.Login;
using Testeger.Shared.DTOs.Requests.Authentication.Register;

namespace UnitTests.Application.Services.Authentication;

public class CustomAuthenticationServiceTests
{
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly JwtSettings _jwtSettings;
    private readonly CustomAuthenticationService _authService;

    public CustomAuthenticationServiceTests()
    {
        _mockTokenService = new Mock<ITokenService>();
#pragma warning disable CS8625
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
#pragma warning restore CS8625
        _jwtSettings = TestUtils.SetupJwtSettings();

        _authService = new CustomAuthenticationService(
            _mockTokenService.Object,
            _mockUserManager.Object,
            _mockRoleManager.Object,
            Options.Create(_jwtSettings)
        );
    }

    [Fact]
    public async Task AddUserToRoleAsync_GivenUserExists_ShouldAddUserToRole()
    {
        var email = "test@example.com";
        var roleName = "TestRole";
        var user = new ApplicationUser { Email = email };

        _mockUserManager.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

        await _authService.AddUserToRoleAsync(email, roleName);

        _mockUserManager.Verify(um => um.AddToRoleAsync(user, roleName), Times.Once);
    }

    [Fact]
    public async Task AddUserToRoleAsync_GivenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        var email = "nonexistent@example.com";
        var roleName = "TestRole";

        _mockUserManager.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);

        await _authService.Invoking(a => a.AddUserToRoleAsync(email, roleName))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("User not found");
    }

    [Fact]
    public async Task AddUserToProjectRoleAsync_GivenUserExistsAndRoleExists_ShouldAddUserToRole()
    {
        var userId = "testUserId";
        var roleName = "TestRole";
        var user = new ApplicationUser { Id = userId };

        _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
        _mockRoleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
        _mockUserManager.Setup(um => um.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

        await _authService.AddUserToProjectRoleAsync(userId, roleName);

        _mockUserManager.Verify(um => um.AddToRoleAsync(user, roleName), Times.Once);
    }

    [Fact]
    public async Task AuthenticateUserAsync_GivenCredentialsAreValid_ShouldReturnTokenDto()
    {
        var request = new UserLoginRequest { Username = "testuser", Password = "password" };
        var user = new ApplicationUser { UserName = request.Username, Email = "test@email.com" };
        var tokenDto = new TokenDto { Token = "testToken", RefreshToken = "refreshToken" };
        var roles = new List<string> { "role", "role2" };

        _mockUserManager.Setup(um => um.FindByNameAsync(request.Username)).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.CheckPasswordAsync(user, request.Password)).ReturnsAsync(true);
        _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
        _mockTokenService.Setup(ts => ts.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(new JwtSecurityToken());
        _mockTokenService.Setup(ts => ts.GenerateRefreshToken()).Returns(tokenDto.RefreshToken);

        var result = await _authService.AuthenticateUserAsync(request);

        result.Should().NotBeNull();
        result.RefreshToken.Should().Be(tokenDto.RefreshToken);
    }

    [Fact]
    public async Task RegisterUserAsync_GivenUserDataIsValid_ShouldRegisterUser()
    {
        var request = new UserRegisterRequest
        {
            UserName = "newuser",
            Email = "newuser@example.com",
            Password = "password",
            PhoneNumber = "1234567890",
            BirthDate = DateTime.Now.AddYears(-20)
        };

        _mockUserManager.Setup(um => um.FindByNameAsync(request.UserName)).ReturnsAsync(null as ApplicationUser);
        _mockUserManager.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(null as ApplicationUser);
        _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);

        await _authService.RegisterUserAsync(request);

 
        _mockUserManager.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), request.Password), Times.Once);
    }

    [Fact]
    public async Task RevokeAsync_GivenUserExists_ShouldRevokeRefreshToken()
    {
        var username = "testuser";
        var user = new ApplicationUser { UserName = username };

        _mockUserManager.Setup(um => um.FindByNameAsync(username)).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

        await _authService.RevokeAsync(username);

        _mockUserManager.Verify(um => um.UpdateAsync(It.Is<ApplicationUser>(u => u.RefreshToken == null)), Times.Once);
    }
}

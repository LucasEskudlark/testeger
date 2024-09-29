using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.Authentication;
using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Requests.Authentication.Login;
using Testeger.Shared.DTOs.Requests.Authentication.Register;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ICustomAuthenticationService _authService;

    public AuthController(ICustomAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(UserLoginRequest request)
    {
        var response = await _authService.AuthenticateUserAsync(request);

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
    {
        await _authService.RegisterUserAsync(request);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(TokenDto request)
    {
        await _authService.RefreshTokenAsync(request);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeAsync(string userName)
    {
        await _authService.RevokeAsync(userName);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRoleAsync(string roleName)
    {
        await _authService.CreateRoleAsync(roleName);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add-user-to-role")]
    public async Task<IActionResult> AddUserToRoleAsync(string email, string roleName)
    {
        await _authService.AddUserToRoleAsync(email, roleName);

        return NoContent();
    }

    [Authorize]
    [HttpPost("reauthenticate")]
    public async Task<IActionResult> ReAuthenticateUserAsync()
    {
        var response = await _authService.ReAuthenticateUserAsync(User);

        return Ok(response);
    }
}

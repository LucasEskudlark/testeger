using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Testeger.Application.Exceptions;
using Testeger.Application.Services.Token;
using Testeger.Application.Settings;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Requests.Authentication.Login;
using Testeger.Shared.DTOs.Requests.Authentication.Register;

namespace Testeger.Application.Services.Authentication;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public CustomAuthenticationService(
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task AddUserToRoleAsync(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new NotFoundException("User not found");

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded) throw new InvalidOperationException($"Unable to add user to role {roleName}");
    }

    public async Task AddUserToProjectRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("User not found");

        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            await CreateRoleAsync(roleName);
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded) throw new InvalidOperationException($"Unable to add user to role {roleName}");
    }

    public async Task<TokenDto> AuthenticateUserAsync(UserLoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new InvalidOperationException("Login failed");
        }

        var authClaims = await GetUserClaims(user);

        var token = _tokenService.GenerateAccessToken(authClaims);

        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenValidityInMinutes);
        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        };
    }

    public async Task CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (roleExist)
        {
            throw new InvalidOperationException($"Role {roleName} already exists");
        }

        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

        if (!roleResult.Succeeded)
        {
            throw new InvalidOperationException($"Error while adding the role {roleName}");
        }
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto request)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token!)
            ?? throw new InvalidOperationException("Invalid access/refresh token");

        var username = principal.Identity?.Name;

        var user = username is not null
            ? await _userManager.FindByNameAsync(username!)
            : throw new NotFoundException($"Identity not found for the token informed"); ;

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new InvalidOperationException("Invalid access/refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
            principal.Claims.ToList());

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        };
    }

    public async Task RegisterUserAsync(UserRegisterRequest request)
    {
        await ValidateUserNameAndEmailAsync(request);

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var userCreationResult = await _userManager.CreateAsync(user, request.Password);

        if (!userCreationResult.Succeeded)
        {
            throw new InvalidOperationException($"Error while creating user.");
        }
    }

    public async Task RevokeAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new NotFoundException("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);
    }

    private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        return authClaims;
    }

    private async Task ValidateUserNameAndEmailAsync(UserRegisterRequest request)
    {
        var userByName = await _userManager.FindByNameAsync(request.UserName);

        if (userByName is not null)
        {
            throw new InvalidOperationException($"Could not create user. User with name {request.UserName} already exists");
        }

        var userByEmail = await _userManager.FindByEmailAsync(request.Email);

        if (userByEmail is not null)
        {
            throw new InvalidOperationException($"Could not create user. User with email {request.Email} already exists");
        }
    }
}

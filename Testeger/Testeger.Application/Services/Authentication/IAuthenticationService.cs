using Testeger.Shared.DTOs.Common;
using Testeger.Shared.DTOs.Requests.Authentication.Login;
using Testeger.Shared.DTOs.Requests.Authentication.Register;

namespace Testeger.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<TokenDto> AuthenticateUserAsync(UserLoginRequest request);
    Task RegisterUserAsync(UserRegisterRequest request);
    Task<TokenDto> RefreshTokenAsync(TokenDto request);
    Task RevokeAsync(string username);
    Task CreateRoleAsync(string roleName);
    Task AddRoleToUserAsync(string email, string roleName);
}

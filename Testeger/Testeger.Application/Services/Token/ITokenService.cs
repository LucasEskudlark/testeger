using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Testeger.Application.Services.Token;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    JwtSecurityToken GenerateInvitationToken(IEnumerable<Claim> claims);
    bool IsTokenExpired(string token);
}

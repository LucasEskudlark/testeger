using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Testeger.Application.Settings;

namespace Testeger.Application.Services.Token;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        return GenerateJwtToken(claims, _jwtSettings.TokenValidityInMinutes);
    }

    public JwtSecurityToken GenerateInvitationToken(IEnumerable<Claim> claims)
    {
        return GenerateJwtToken(claims, _jwtSettings.InvitationTokenValidityInMinutes);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims, double tokenValidity)
    {
        var secretKey = GetSecretKey();

        var privateKey = Encoding.UTF8.GetBytes(secretKey);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(tokenValidity),
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(secureRandomBytes);

        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var secretKey = GetSecretKey();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public bool IsTokenExpired(string token)
    {
        try
        {
            var jwtToken = new JwtSecurityToken(token.Replace("\"", ""));
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    private string GetSecretKey()
    {
        var secretKey = _jwtSettings.SecretKey;

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("Invalid secret key");
        }

        return secretKey;
    }
}

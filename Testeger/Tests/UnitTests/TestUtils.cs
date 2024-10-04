using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UnitTests;

public static class TestUtils
{
    public static JwtSecurityToken MockJwtToken(IEnumerable<Claim> claims)
    {
        var secretKey = "model-secret-key-with-enough-bits-for-sha256";

        var privateKey = Encoding.UTF8.GetBytes(secretKey);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Audience = "mock-audience",
            Issuer = "mock-issuer",
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }

    public static JwtSettings SetupJwtSettings()
    {
        return new()
        {
            Audience = "TestAudience",
            Issuer = "TestIssues",
            SecretKey = "model-secret-key-with-enough-bits-for-sha256",
            TokenValidityInMinutes = 1,
            InvitationTokenValidityInMinutes = 1,
            RefreshTokenValidityInMinutes = 1
        };
    }
}

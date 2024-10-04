using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Testeger.Application.Services.Token;

namespace UnitTests.Application.Services.Token;

public class TokenServiceTests
{
    private readonly JwtSettings _jwtSettings;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        _jwtSettings = TestUtils.SetupJwtSettings();
        IOptions<JwtSettings> options = Options.Create(_jwtSettings);
        _tokenService = new(options);
    }

    [Fact]
    public void GenerateAccessToken_ShouldCreateValidJwtToken()
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };
        var token = _tokenService.GenerateAccessToken(claims);

        token.Should().NotBeNull();
        token.Issuer.Should().Be(_jwtSettings.Issuer);
        token.Audiences.Should().Contain(_jwtSettings.Audience);
        token.Claims.Should().Contain(c => c.Type == "unique_name" && c.Value == "testuser");
        token.Claims.Should().Contain(c => c.Type == "email" && c.Value == "testuser@example.com");
        token.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes), TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void GenerateInvitationToken_ShouldCreateValidJwtToken()
    {
        var claims = new[]
        {
                new Claim("InvitationId", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, "invitee@example.com")
            };

        var token = _tokenService.GenerateInvitationToken(claims);

        token.Should().NotBeNull();
        token.Issuer.Should().Be(_jwtSettings.Issuer);
        token.Audiences.Should().Contain(_jwtSettings.Audience);
        token.Claims.Should().Contain(c => c.Type == "InvitationId");
        token.Claims.Should().Contain(c => c.Type == "email" && c.Value == "invitee@example.com");
        token.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(_jwtSettings.InvitationTokenValidityInMinutes), TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnUniqueStrings()
    {
        var token1 = _tokenService.GenerateRefreshToken();
        var token2 = _tokenService.GenerateRefreshToken();

        token1.Should().NotBeNullOrEmpty();
        token2.Should().NotBeNullOrEmpty();
        token1.Should().NotBe(token2);
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ShouldReturnValidPrincipal()
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };
        var token = _tokenService.GenerateAccessToken(claims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenString);

        principal.Should().NotBeNull();
        principal.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "testuser");
        principal.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "testuser@example.com");
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ShouldThrowExceptionForInvalidToken()
    {
        var invalidToken = "invalid.token.string";

        _tokenService.Invoking(service => service.GetPrincipalFromExpiredToken(invalidToken)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsTokenExpired_ShouldReturnFalseForValidToken()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "testuser") };
        var token = _tokenService.GenerateAccessToken(claims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var isExpired = _tokenService.IsTokenExpired(tokenString);

        isExpired.Should().BeFalse();
    }

    [Fact]
    public void IsTokenExpired_ShouldReturnTrueForExpiredToken()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "testuser") };
        var expiredToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(-5),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!)),
                SecurityAlgorithms.HmacSha256Signature)
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(expiredToken);

        var isExpired = _tokenService.IsTokenExpired(tokenString);

        isExpired.Should().BeTrue();
    }
}

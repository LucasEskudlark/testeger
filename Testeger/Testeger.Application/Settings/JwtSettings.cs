namespace Testeger.Application.Settings;

public sealed class JwtSettings
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public string? SecretKey { get; set; }
    public double TokenValidityInMinutes { get; set; }
    public double RefreshTokenValidityInMinutes { get; set; }
}

namespace Testeger.Application.Settings;

public sealed class JwtSettings
{
    public string? Audience { get; init; }
    public string? Issuer { get; init; }
    public string? SecretKey { get; init; }
    public double TokenValidityInMinutes { get; init; }
    public double InvitationTokenValidityInMinutes { get; init; }
    public double RefreshTokenValidityInMinutes { get; init; }
}

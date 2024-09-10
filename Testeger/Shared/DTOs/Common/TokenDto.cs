namespace Testeger.Shared.DTOs.Common;

public class TokenDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? Expiration { get; set; }
}

namespace Testeger.Application.Settings;

public sealed class SmtpSettings
{
    public required string Server { get; init; }
    public required int Port { get; init; }
    public required bool UseSsl { get; init; } = true;
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string SenderName { get; init; }
    public required string SenderEmail { get; init; }
}

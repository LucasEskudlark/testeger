namespace Testeger.Shared.DTOs.Requests.Authentication.Login;

public class UserLoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

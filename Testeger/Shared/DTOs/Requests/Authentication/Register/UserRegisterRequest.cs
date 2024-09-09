namespace Testeger.Shared.DTOs.Requests.Authentication.Register;

public class UserRegisterRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}

using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class UserInvitationRequest
{
    public required string Email { get; set; }
    public required RoleType RoleType { get; set; }
}

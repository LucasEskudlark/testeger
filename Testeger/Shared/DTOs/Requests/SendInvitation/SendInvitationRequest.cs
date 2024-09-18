namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class SendInvitationRequest
{
    public required List<UserInvitationRequest> Users { get; set; }
    public required string ProjectId { get; set; }

}

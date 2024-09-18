namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class SendInvitationRequest
{
    public required List<string> Emails { get; set; }
    public required string ProjectId { get; set; }

}

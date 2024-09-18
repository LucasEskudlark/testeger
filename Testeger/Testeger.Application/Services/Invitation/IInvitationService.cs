using Testeger.Shared.DTOs.Requests.SendInvitation;

namespace Testeger.Application.Services.Invitation;

public interface IInvitationService
{
    Task SendInvitationAsync(SendInvitationRequest request);
}

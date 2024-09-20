using System.Security.Claims;
using Testeger.Shared.DTOs.Requests.ConfirmInvitation;
using Testeger.Shared.DTOs.Requests.SendInvitation;
using Testeger.Shared.DTOs.Responses.Invitation;

namespace Testeger.Application.Services.Invitation;

public interface IInvitationService
{
    Task SendInvitationAsync(SendInvitationRequest request);
    Task<ConfirmInvitationResponse> ConfirmInvitationAsync(ConfirmInvitationRequest request, ClaimsPrincipal user);
}

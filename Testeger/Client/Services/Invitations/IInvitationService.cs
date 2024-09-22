using Testeger.Client.ViewModels.Users;
using Testeger.Shared.DTOs.Responses.Invitation;

namespace Testeger.Client.Services.Invitations;

public interface IInvitationService
{
    Task SendProjectInvitationAsync(IEnumerable<UserInvitationViewModel> users, string projectId);
    Task<ConfirmInvitationResponse> ConfirmProjectInvitationAsync(string token);
}

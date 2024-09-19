using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.Services.Invitations;

public interface IInvitationService
{
    Task SendProjectInvitationAsync(IEnumerable<UserInvitationViewModel> users, string projectId);
}

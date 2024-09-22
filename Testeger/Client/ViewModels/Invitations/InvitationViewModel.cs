using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.ViewModels.Invitations;

public class InvitationViewModel
{
    public IEnumerable<UserInvitationViewModel>? Users { get; set; }
    public string? ProjectId { get; set; }
}

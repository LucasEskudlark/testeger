using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class UserInvitationRequestValidator : AbstractValidator<UserInvitationRequest>
{
    public UserInvitationRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(r => r.RoleType)
            .IsInEnum();
    }
}

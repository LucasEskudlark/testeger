using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.ConfirmInvitation;

public class ConfirmInvitationRequestValidator : AbstractValidator<ConfirmInvitationRequest>
{
    public ConfirmInvitationRequestValidator()
    {
        RuleFor(r => r.Token)
            .NotEmpty();
    }
}

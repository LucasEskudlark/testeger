using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class SendInvitationRequestValidator : AbstractValidator<SendInvitationRequest>
{
    public SendInvitationRequestValidator()
    {
        RuleFor(r => r.Users)
            .ForEach(e => e.SetValidator(new UserInvitationRequestValidator()))
            .NotEmpty();

        RuleFor(r => r.ProjectId)
            .NotEmpty()
            .MaximumLength(36);
    }
}

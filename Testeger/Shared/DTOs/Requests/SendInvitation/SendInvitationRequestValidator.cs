using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.SendInvitation;

public class SendInvitationRequestValidator : AbstractValidator<SendInvitationRequest>
{
    public SendInvitationRequestValidator()
    {
        RuleFor(r => r.Emails)
            .ForEach(e => e.EmailAddress())
            .NotEmpty();

        RuleFor(r => r.ProjectId)
            .NotEmpty()
            .MaximumLength(36);
    }
}

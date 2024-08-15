using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.FinishTestCaseResult;

public class FinishTestCaseResultRequestValidator : AbstractValidator<FinishTestCaseResultRequest>
{
    public FinishTestCaseResultRequestValidator()
    {
        RuleFor(r => r.Id)
            .MaximumLength(36);

        RuleFor(r => r.TestCaseId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.ActualResult)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(1500);

        RuleFor(r => r.ElapsedTime)
            .NotEmpty();

        RuleFor(r => r.IsSuccess)
            .NotEmpty();
    }
}

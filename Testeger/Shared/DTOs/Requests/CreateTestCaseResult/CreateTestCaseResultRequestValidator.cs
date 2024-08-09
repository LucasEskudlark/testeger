using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.CreateTestCaseResult;

public class CreateTestCaseResultRequestValidator : AbstractValidator<CreateTestCaseResultRequest>
{
    public CreateTestCaseResultRequestValidator()
    {
        RuleFor(r => r.TestCaseId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.ActualResult)
            .MinimumLength(5)
            .MaximumLength(1500);
    }
}

using FluentValidation;

namespace Testeger.Application.DTOs.Requests.Common;

public class TestCaseDetailsRequestValidator : AbstractValidator<TestCaseDetailsRequest>
{
    public TestCaseDetailsRequestValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(1500);

        RuleFor(r => r.PreConditions)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(700);

        RuleFor(r => r.Steps)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(700);

        RuleFor(r => r.ExpectedResult)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(700);

        RuleFor(r => r.Environment)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);
    }
}

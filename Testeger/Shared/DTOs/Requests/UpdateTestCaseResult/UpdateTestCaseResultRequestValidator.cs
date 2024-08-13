using FluentValidation;

namespace Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;

public class UpdateTestCaseResultRequestValidator :AbstractValidator<UpdateTestCaseResultRequest>
{
    public UpdateTestCaseResultRequestValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.TestCaseId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.ActualResult)
            .MinimumLength(5)
            .MaximumLength(1500);

        RuleFor(r => r.ElapsedTime)
            .NotEmpty();

        RuleFor(r => r.IsSuccess)
            .NotEmpty();
    }
}

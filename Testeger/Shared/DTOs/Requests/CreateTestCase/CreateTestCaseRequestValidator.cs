using FluentValidation;
using Testeger.Shared.DTOs.Requests.Common;

namespace Testeger.Shared.DTOs.Requests.CreateTestCase;

public class CreateTestCaseRequestValidator : AbstractValidator<CreateTestCaseRequest>
{
    public CreateTestCaseRequestValidator()
    {
        RuleFor(r => r.TestRequestId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.ProjectId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.UserId)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(r => r.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);

        RuleFor(r => r.Details)
            .NotEmpty()
            .SetValidator(new TestCaseDetailsRequestValidator());
    }
}

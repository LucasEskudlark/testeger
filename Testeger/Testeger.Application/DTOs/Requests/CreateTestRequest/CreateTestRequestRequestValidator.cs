using FluentValidation;

namespace Testeger.Application.DTOs.Requests.CreateTestRequest;

public class CreateTestRequestRequestValidator : AbstractValidator<CreateTestRequestRequest>
{
    public CreateTestRequestRequestValidator()
    {
        var currentDate = DateTime.Now;

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

        RuleFor(r => r.Description)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(1500);

        RuleFor(r => r.StoryLink)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(512);

        RuleFor(r => r.Status)
            .IsInEnum();

        RuleFor(r => r.PriorityLevel)
            .IsInEnum();

        RuleFor(r => r.DueDate)
            .NotEmpty()
            .GreaterThan(currentDate);
    }
}

using FluentValidation;

namespace Testeger.Application.Requests.CreateProject;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.UserId)
            .NotEmpty()
            .MaximumLength(36);
    }
}

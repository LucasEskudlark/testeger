using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Testeger.Application.DTOs.Requests.Common;

public class ImageFileValidator : AbstractValidator<IFormFile>
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    public ImageFileValidator()
    {
        RuleFor(f => f.Length)
            .NotEmpty()
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage("File size is larger than allowed");

        RuleFor(f => f.ContentType)
            .NotEmpty()
            .Must(ct => ct.Equals("image/jpeg") || ct.Equals("image/jpg") || ct.Equals("image/png"))
            .WithMessage("File extension must be .jpeg, .jpg or .png!");
    }
}

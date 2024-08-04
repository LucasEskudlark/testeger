using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.Requests.CreateProject;

namespace Testeger.Application.Configuration;

public static class RequestValidatorsConfiguration
{
    public static void AddRequestValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
    }
}

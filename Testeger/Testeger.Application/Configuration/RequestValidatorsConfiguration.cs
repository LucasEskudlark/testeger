using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Requests.CreateTestRequest;

namespace Testeger.Application.Configuration;

public static class RequestValidatorsConfiguration
{
    public static void AddRequestValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
        services.AddScoped<IValidator<CreateTestRequestRequest>, CreateTestRequestRequestValidator>();
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;

namespace Testeger.Application.Configuration;

public static class RequestValidatorsConfiguration
{
    public static void AddRequestValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
        services.AddScoped<IValidator<CreateTestRequestRequest>, CreateTestRequestRequestValidator>();
        services.AddScoped<IValidator<CreateTestCaseRequest>, CreateTestCaseRequestValidator>();
        services.AddScoped<IValidator<CreateTestCaseResultRequest>, CreateTestCaseResultRequestValidator>();
    }
}

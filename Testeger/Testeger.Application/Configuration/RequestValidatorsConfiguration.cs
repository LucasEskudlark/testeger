using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.DTOs.Requests.CreateTestRequest;

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

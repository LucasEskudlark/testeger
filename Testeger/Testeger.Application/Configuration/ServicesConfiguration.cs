using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.Services.Image;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.TestCase;
using Testeger.Application.Services.TestCaseResult;
using Testeger.Application.Services.TestRequest;

namespace Testeger.Application.Configuration;

public static class ServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITestRequestService, TestRequestService>();
        services.AddScoped<ITestCaseService, TestCaseService>();
        services.AddScoped<ITestCaseResultService, TestCaseResultService>();
        services.AddScoped<IImageService, ImageService>();
    }
}

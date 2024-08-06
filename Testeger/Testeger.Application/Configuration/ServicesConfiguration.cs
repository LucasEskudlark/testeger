using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.TestRequest;

namespace Testeger.Application.Configuration;

public static class ServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITestRequestService, TestRequestService>();
    }
}

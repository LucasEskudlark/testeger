using Testeger.Client.Services.Images;
using Testeger.Client.Services.Notifications;
using Testeger.Client.Services.Projects;
using Testeger.Client.Services.TestCaseResults;
using Testeger.Client.Services.TestCases;
using Testeger.Client.Services.TestRequests;

namespace Testeger.Client.Configuration;

public static class ClientServicesConfiguration
{
    public static void AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectServiceNV, ProjectServiceNV>();
        services.AddScoped<ITestRequestServiceNV, TestRequestServiceNV>();
        services.AddScoped<ITestCaseServiceNV, TestCaseServiceNV>();
        services.AddScoped<ITestCaseResultService, TestCaseResultService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<INotificationService, NotificationService>();
    }
}

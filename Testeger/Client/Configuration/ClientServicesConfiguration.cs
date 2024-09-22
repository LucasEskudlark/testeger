using Testeger.Client.Services.Authentication;
using Testeger.Client.Services.Authorization;
using Testeger.Client.Services.Images;
using Testeger.Client.Services.Invitations;
using Testeger.Client.Services.Notifications;
using Testeger.Client.Services.Projects;
using Testeger.Client.Services.TestCaseResults;
using Testeger.Client.Services.TestCases;
using Testeger.Client.Services.TestRequests;
using Testeger.Client.Services.Users;

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
        services.AddScoped<ICustomAuthService, CustomAuthService>();
        services.AddScoped<IPolicyValidatorService, PolicyValidatorService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IInvitationService, InvitationService>();
    }
}

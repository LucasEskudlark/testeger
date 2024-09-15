using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Image;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.TestCase;
using Testeger.Application.Services.TestCaseResult;
using Testeger.Application.Services.TestRequest;
using Testeger.Application.Services.Token;
using Testeger.Application.Services.User;

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
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
    }
}

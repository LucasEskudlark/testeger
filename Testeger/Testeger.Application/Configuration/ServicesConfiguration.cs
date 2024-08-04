using Microsoft.Extensions.DependencyInjection;
using Testeger.Application.Services.Project;

namespace Testeger.Application.Configuration;

public static class ServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
    }
}

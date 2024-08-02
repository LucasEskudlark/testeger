using Microsoft.Extensions.DependencyInjection;
using Testeger.Infra.Repositories;
using Testeger.Infra.Repositories.Images;
using Testeger.Infra.Repositories.Projects;
using Testeger.Infra.Repositories.TestCaseResults;
using Testeger.Infra.Repositories.TestCases;
using Testeger.Infra.Repositories.TestRequests;

namespace Testeger.Infra.Configuration;

public static class RepositoriesConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITestCaseRepository, TestCaseRepository>();
        services.AddScoped<ITestRequestRepository, TestRequestRepository>();
        services.AddScoped<ITestCaseResultRepository, TestCaseResultRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
    }
}

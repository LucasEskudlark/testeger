namespace Testeger.Api.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this IServiceCollection services, string blazorPolicy)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: blazorPolicy,
                policy =>
                {
                    policy.WithOrigins("https://localhost:50351")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                });
        });
    }
}

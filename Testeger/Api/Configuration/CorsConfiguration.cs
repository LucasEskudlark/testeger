using Testeger.Application.Settings;

namespace Testeger.Api.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration, string blazorPolicy)
    {
        var clientAddress = configuration[$"{nameof(ClientSettings)}:{nameof(ClientSettings.BaseAddress)}"]
            ?? throw new ArgumentNullException("Client address can not be null!");

        services.AddCors(options =>
        {
            options.AddPolicy(name: blazorPolicy,
                policy =>
                {
                    policy.WithOrigins(clientAddress)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                });
        });
    }
}

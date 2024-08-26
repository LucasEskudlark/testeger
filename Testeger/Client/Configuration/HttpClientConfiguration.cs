namespace Testeger.Client.Configuration;

public static class HttpClientConfiguration
{
    private const string BaseAddressPath = "ApiSettings:BaseAddress";

    public static void SetupHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration[BaseAddressPath]
            ?? throw new InvalidOperationException("BaseAddress is not configured");

        services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            });

        services.AddHttpClient();
    }
}

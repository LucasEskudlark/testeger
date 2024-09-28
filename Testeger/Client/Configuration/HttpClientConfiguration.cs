namespace Testeger.Client.Configuration;

public static class HttpClientConfiguration
{
    public static void SetupHttpClient(this IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            });

        services.AddHttpClient();
    }
}

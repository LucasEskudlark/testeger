using System.Net.Http.Headers;

namespace Testeger.Client.Configuration;

public static class HttpClientConfiguration
{
    public static void SetupHttpClient(this IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp =>
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        });

        services.AddHttpClient();
    }
}

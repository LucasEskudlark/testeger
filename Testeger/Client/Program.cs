using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Text.Json;
using Testeger.Client;
using Testeger.Client.Authentication;
using Testeger.Client.Authorization;
using Testeger.Client.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true, reloadOnChange: true);

ConfigurationData configData = new();
try
{
    using var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    var response = await httpClient.GetAsync("api/configuration");

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"Failed to fetch configuration data. Status Code: {response.StatusCode}");
    }
    var json = await response.Content.ReadAsStringAsync();
    configData = JsonSerializer.Deserialize<ConfigurationData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
}
catch (Exception ex)
{
    Console.WriteLine($"Error fetching configuration: {ex.Message}");
}

var apiUrl = configData?.ApiUrl;

builder.Services.AddRadzenComponents();
builder.Services.AddClientServices();
builder.Services.SetupHttpClient(apiUrl);
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
       provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, ProjectRolePolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, ProjectRoleAuthorizationHandler>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();

public class ConfigurationData
{
    public string? ApiUrl { get; set; }
}

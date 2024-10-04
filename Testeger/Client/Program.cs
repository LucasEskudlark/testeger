using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Net.Http.Json;
using Testeger.Client;
using Testeger.Client.Authentication;
using Testeger.Client.Authorization;
using Testeger.Client.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiSettings = await FetchApiConfiguration(builder.HostEnvironment.BaseAddress);

var configDictionary = new Dictionary<string, string>
    {
        { "ApiSettings:ApiBaseAddress", apiSettings.ApiBaseAddress }
    };

builder.Configuration.AddInMemoryCollection(configDictionary);

builder.Services.AddRadzenComponents();
builder.Services.AddClientServices();
builder.Services.SetupHttpClient(apiSettings.ApiBaseAddress);
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
       provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, ProjectRolePolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, ProjectRoleAuthorizationHandler>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();

static async Task<ApiSettings> FetchApiConfiguration(string baseAddress)
{
    try
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        var response = await httpClient.GetFromJsonAsync<ApiSettings>("api/configuration");
        return response ?? new ApiSettings();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching configuration: {ex.Message}");
        return new ApiSettings();
    }
}

public class ApiSettings
{
    public string? ApiBaseAddress { get; set; }
}

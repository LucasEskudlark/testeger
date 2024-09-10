using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Testeger.Client;
using Testeger.Client.Authentication;
using Testeger.Client.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddRadzenComponents();
builder.Services.AddClientServices();
builder.Services.SetupHttpClient(builder.Configuration);
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
       provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();

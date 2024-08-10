using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Testeger.Client;
using Testeger.Client.Services.Projects;
using Testeger.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TestRequestService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<TestCaseService>();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped<IProjectServiceNV, ProjectServiceNV>();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5000")
    });
builder.Services.AddHttpClient();


await builder.Build().RunAsync();

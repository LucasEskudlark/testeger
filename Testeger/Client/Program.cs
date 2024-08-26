using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Testeger.Client;
using Testeger.Client.Services.Images;
using Testeger.Client.Services.Projects;
using Testeger.Client.Services.TestCaseResults;
using Testeger.Client.Services.TestCases;
using Testeger.Client.Services.TestRequests;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<IProjectServiceNV, ProjectServiceNV>();
builder.Services.AddScoped<ITestRequestServiceNV, TestRequestServiceNV>();
builder.Services.AddScoped<ITestCaseServiceNV, TestCaseServiceNV>();
builder.Services.AddScoped<ITestCaseResultService, TestCaseResultService>();
builder.Services.AddScoped<IImageService, ImageService>();

var baseAddress = builder.Configuration["ApiSettings:BaseAddress"]
    ?? throw new InvalidOperationException("BaseAddress is not configured");

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(baseAddress)
    });
builder.Services.AddHttpClient();

await builder.Build().RunAsync();

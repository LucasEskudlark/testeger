using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Testeger.Api.Configuration;
using Testeger.Api.Middlewares;
using Testeger.Application.Configuration;
using Testeger.Application.MappingProfiles;
using Testeger.Application.Settings;
using Testeger.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

builder.Configuration.AddConfiguration(configuration);
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
    });
builder.Services.AddRazorPages();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddRequestValidators();
builder.Services.AddAutoMapper(typeof(ProjectMappingProfile));

var BlazorClientPolicy = "BlazorClientPolicy";
builder.Services.ConfigureCors(BlazorClientPolicy);
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCustomExceptionMiddleware();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthentication();

app.UseCors(BlazorClientPolicy);

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

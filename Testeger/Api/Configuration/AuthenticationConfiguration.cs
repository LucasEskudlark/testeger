using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Testeger.Api.Configuration;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = GetSecretKey(configuration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration["JwtSettings:Audience"],
                ValidIssuer = configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

    private static string GetSecretKey(IConfiguration configuration)
    {
        var secretKey = configuration["JwtSettings:SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("Invalid secret key");
        }

        return secretKey;
    }
}
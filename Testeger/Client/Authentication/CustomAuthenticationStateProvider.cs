using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Testeger.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("token");
        var identity = await GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    private async Task<ClaimsIdentity> GetClaimsIdentity(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
        {
            await _localStorage.RemoveItemAsync("token");
            return new ClaimsIdentity();
        }

        var claims = ParseClaimsFromJwt(token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

        return new ClaimsIdentity(claims, "jwt");
    }

    private static bool IsTokenExpired(string token)
    {
        try
        {
            var jwtToken = new JwtSecurityToken(token.Replace("\"", ""));
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

        if (keyValuePairs.TryGetValue("role", out var roleValue))
        {
            claims = claims.Append(new Claim(ClaimTypes.Role, roleValue.ToString()));
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        await _localStorage.RemoveItemAsync("token");
        NotifyAuthenticationStateChanged(authState);
    }
}
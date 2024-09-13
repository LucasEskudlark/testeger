using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Testeger.Client.Authentication;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels.Authentication;
using Testeger.Shared.DTOs.Common;

namespace Testeger.Client.Services.Authentication;

public class CustomAuthService : BaseService, ICustomAuthService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private const string BaseAddress = "api/auth";

    public CustomAuthService(
        HttpClient httpClient,
        INotificationService notificationService,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider)
        : base(httpClient, notificationService)
    {
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<AuthResult> LoginAsync(LoginViewModel loginViewModel)
    {
        var address = BaseAddress + "/login";

        var response = await _httpClient.PostAsJsonAsync(address, loginViewModel);
        var loginResponse = await response.Content.ReadFromJsonAsync<TokenDto>();

        if (!response.IsSuccessStatusCode || loginResponse is null)
        {
            _notificationService.ShowFailNotification("Error", "Could not login.");
            return new AuthResult { Success = false };
        }

        var token = loginResponse.Token;

        await _localStorageService.SetItemAsync("token", token);
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthResult { Success = true };
    }

    public async Task<AuthResult> RegisterAsync(RegisterViewModel registerViewModel)
    {
        var address = BaseAddress + "/register";

        var response = await _httpClient.PostAsJsonAsync(address, registerViewModel);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not register.");
            return new AuthResult { Success = false };
        }

        return new AuthResult { Success = true };
    }

    public async Task<AuthResult> ReAuthenticateUserAsync()
    {
        var address = BaseAddress + "/reauthenticate";

        var response = await _httpClient.PostAsync(address, null);
        var reAuthResponse = await response.Content.ReadFromJsonAsync<TokenDto>();

        if (!response.IsSuccessStatusCode || reAuthResponse is null)
        {
            _notificationService.ShowFailNotification("Error", "Please log in again to be able to access the new project.");
            return new AuthResult { Success = false };
        }

        var token = reAuthResponse.Token;

        await _localStorageService.SetItemAsync("token", token);
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthResult { Success = true };
    }
}

public class AuthResult
{
    public bool Success { get; set; }
}

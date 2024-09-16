using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.Services.Users;

public class UserService : BaseService, IUserService
{
    private readonly AuthenticationStateProvider _stateProvider;
    private const string BaseAddress = "api/users";
    private const string DefaultUsername = "Guest";
    private const string NameClaimType = "unique_name";

    public UserService(
        HttpClient httpClient,
        INotificationService notificationService,
        AuthenticationStateProvider stateProvider)
        : base(httpClient, notificationService)
    {
        _stateProvider = stateProvider;
    }

    public async Task<string> GetUserNameAsync()
    {
        var authState = await _stateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null && !user.Identity.IsAuthenticated)
        {
            return DefaultUsername;
        }

        ClaimsIdentity identity = user.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var username = claims.FirstOrDefault(c => c.Type == NameClaimType).Value;

        return username ?? DefaultUsername;
    }

    public async Task<IEnumerable<UserNameIdViewModel>> GetUsersByRoleAsync(string projectId, string roleName)
    {
        int insertPosition = roleName.IndexOf(':') + 1;
        string role = roleName.Insert(insertPosition, projectId + ":");

        var address = BaseAddress + $"/role/{role}";

        var response = await _httpClient.GetFromJsonAsync<IEnumerable<UserNameIdViewModel>>(address);

        return response;
    }

    public async Task<UserNameIdViewModel> GetUserByIdAsync(string userId)
    {
        var address = BaseAddress + $"/{userId}";

        var response = await _httpClient.GetFromJsonAsync<UserNameIdViewModel>(address);

        return response;
    }
}

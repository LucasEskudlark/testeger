﻿using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Testeger.Client.Services.Notifications;

namespace Testeger.Client.Services.Users;

public class UserService : BaseService, IUserService
{
    private readonly AuthenticationStateProvider _stateProvider;
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
}

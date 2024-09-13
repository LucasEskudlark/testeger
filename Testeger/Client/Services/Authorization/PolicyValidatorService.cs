using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Testeger.Client.Services.Authorization;

public class PolicyValidatorService : IPolicyValidatorService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public PolicyValidatorService(IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider)
    {
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> ValidatePolicyAsync(string policyName)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        var authResult = await _authorizationService.AuthorizeAsync(user, policyName);
        
        return authResult.Succeeded;
    }
}

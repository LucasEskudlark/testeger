using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Testeger.Client.Authorization;

public class ProjectRolePolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

    public ProjectRolePolicyProvider(IOptions<AuthorizationOptions> options)
    {
        _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => _fallbackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("ProjectRole:", StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new ProjectRoleRequirement(policyName));
            return Task.FromResult(policy.Build());
        }

        return _fallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}

public class ProjectRoleRequirement : IAuthorizationRequirement
{
    public string Roles { get; }

    public ProjectRoleRequirement(string policyName)
    {
        Roles = policyName;
    }
}
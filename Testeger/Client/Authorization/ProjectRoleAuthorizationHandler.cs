using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Text.Json;


namespace Testeger.Client.Authorization;

public class ProjectRoleAuthorizationHandler : AuthorizationHandler<ProjectRoleRequirement>
{
    private readonly NavigationManager _navigationManager;

    public ProjectRoleAuthorizationHandler(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectRoleRequirement requirement)
    {
        var projectId = GetProjectIdFromUrl();
        if (string.IsNullOrEmpty(projectId))
        {
            return Task.CompletedTask;
        }

        var requiredRoles = requirement.Roles
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(role => role.Trim().Replace("ProjectRole:", $"ProjectRole:{projectId}:"))
            .ToList();

        var roleClaim = context.User.FindFirst(c => c.Type == "role");
        if (roleClaim == null)
        {
            return Task.CompletedTask;
        }

        var userRoles = DeserializeRoles(roleClaim.Value);

        if (userRoles.Any(requiredRoles.Contains))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private string GetProjectIdFromUrl()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
        var segments = uri.Segments;
        if (segments.Length > 2 && segments[1].Equals("project/", StringComparison.OrdinalIgnoreCase))
        {
            return segments[2].TrimEnd('/');
        }
        return null;
    }

    private static IEnumerable<string> DeserializeRoles(string roleClaimValue)
    {
        try
        {
            var rolesArray = JsonSerializer.Deserialize<string[]>(roleClaimValue);
            return rolesArray ?? Enumerable.Empty<string>();
        }
        catch (JsonException)
        {
            return [roleClaimValue];
        }
    }
}
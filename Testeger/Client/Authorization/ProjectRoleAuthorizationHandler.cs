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

        var requiredRoles = new List<string>();
        var roleArray = requirement.Roles.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());

        foreach (var role in roleArray)
        {
            var requiredRole = role.Replace("ProjectRole:", "ProjectRole:" + projectId + ":");
            requiredRoles.Add(requiredRole);
        }

        var roleClaim = context.User.FindFirst(c => c.Type == "role");

        var rolesArray = JsonSerializer.Deserialize<string[]>(roleClaim.Value);

        if (rolesArray != null && rolesArray.Any(requiredRoles.Contains))
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
}
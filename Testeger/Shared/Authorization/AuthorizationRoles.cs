using System.Reflection.Metadata;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.Authorization;

public static class AuthorizationRoles
{
    public const string Manager = "ProjectRole:Manager";
    public const string Developer = "ProjectRole:Developer";
    public const string QA = "ProjectRole:QA";
    
    public static string GetRoleForProject(string projectId, string roleName)
    {
        int lastColonIndex = roleName.LastIndexOf(':');

        if (lastColonIndex == -1)
        {
            throw new ArgumentException("Input string does not contain a colon.");
        }

        string prefix = roleName[..lastColonIndex];
        string suffix = roleName[(lastColonIndex + 1)..];

        return $"{prefix}:{projectId}:{suffix}";
    }

    public static string GetRoleFromRoleType(RoleType roleType)
    {
        return roleType switch
        {
            RoleType.ProjectManager => Manager,
            RoleType.QA => QA,
            _ => Developer,
        };
    }
}

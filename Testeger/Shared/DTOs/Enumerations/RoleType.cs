using System.ComponentModel;

namespace Testeger.Shared.DTOs.Enumerations;

public enum RoleType
{
    [Description("Manager")]
    ProjectManager = 0,

    [Description("Q.A")]
    QA = 1,

    [Description("Developer")]
    Developer = 2
}

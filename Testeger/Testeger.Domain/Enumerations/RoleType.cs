using System.ComponentModel;

namespace Testeger.Domain.Enumerations;

public enum RoleType
{
    [Description("Project Manager")]
    ProjectManager = 0,

    [Description("Quality Assurance Professional")]
    QA = 1,

    [Description("Software Developer")]
    Developer = 2
}

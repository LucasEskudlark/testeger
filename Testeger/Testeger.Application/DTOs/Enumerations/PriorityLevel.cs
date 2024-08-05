using System.ComponentModel;

namespace Testeger.Application.DTOs.Enumerations;

public enum PriorityLevel
{
    [Description("Low priority - can be addressed at a later time.")]
    Low = 0,

    [Description("Medium priority - should be addressed in due time.")]
    Medium = 1,

    [Description("High priority - requires prompt attention.")]
    High = 2,

    [Description("Urgent priority - needs immediate attention.")]
    Urgent = 3
}

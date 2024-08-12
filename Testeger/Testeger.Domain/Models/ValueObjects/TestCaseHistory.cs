using Testeger.Domain.Enumerations;

namespace Testeger.Domain.Models.ValueObjects;

public class TestCaseHistory
{
    public string? ChangedByUserId { get; set; }
    public TestCaseStatus? OldStatus { get; set; }
    public TestCaseStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

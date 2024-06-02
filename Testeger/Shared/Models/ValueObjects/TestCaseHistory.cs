using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Models.ValueObjects;

public class TestCaseHistory
{
    public string? TestCaseId { get; set; }
    public string? ChangedByUserId { get; set; }
    public TestCaseStatus? OldStatus { get; set; }
    public TestCaseStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

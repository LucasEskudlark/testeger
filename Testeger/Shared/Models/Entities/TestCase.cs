using Testeger.Shared.Models.Enumerations;
using Testeger.Shared.Models.ValueObjects;

namespace Testeger.Shared.Models.Entities;

public class TestCase
{
    public TestCase()
    {
        SavedTimes = new List<string> ();
        ElapsedTime = TimeSpan.Zero;
    }
    public string? Id { get; set; }
    public string? RequestId { get; set; }
    public string? CreatedBy { get; set; }
    public string? Title { get; set; }
    public TestCaseDetails? Details { get; set; }
    public TestCaseResults? Results { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public List<string> SavedTimes { get; set; }
    public bool IsCompleted { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan AmountOfTimeSpentToTest { get; set; }
}

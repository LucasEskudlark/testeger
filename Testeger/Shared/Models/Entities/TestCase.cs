using Testeger.Shared.Models.Enumerations;
using Testeger.Shared.Models.ValueObjects;

namespace Testeger.Shared.Models.Entities;

public class TestCase
{
    public TestCase()
    {
        History = new List<TestCaseHistory>();
        Results = new List<TestCaseResults>();
    }

    public string? Id { get; set; }
    public string? RequestId { get; set; }
    public string? ProjectId { get; set; }
    public string? CreatedBy { get; set; }
    public string? Title { get; set; }
    public TestCaseDetails? Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public List<TestCaseHistory> History { get; set; }
    public List<TestCaseResults> Results { get; set; }

    public bool IsCompleted => Status == TestCaseStatus.Completed;

    public TestCaseResults GetLatestTestCaseResult() => Results.LastOrDefault() ?? new TestCaseResults();
}

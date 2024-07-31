using Testeger.Domain.Models.Entities;

namespace Testeger.Shared.Models.ValueObjects;

public class TestCaseResult
{
    public TestCaseResult()
    {
        SavedTimes = new List<string>();
        ElapsedTime = TimeSpan.Zero;
    }

    public string? ActualResult { get; set; }
    public IEnumerable<string> SavedTimes { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan AmountOfTimeSpentToTest { get; set; }
    public bool IsSuccess { get; set; }

    public ICollection<Image>? Images { get; set; }
}

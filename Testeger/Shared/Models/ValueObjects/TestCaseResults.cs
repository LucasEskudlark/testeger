using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Models.ValueObjects;

public class TestCaseResults
{
    public TestCaseResults()
    {
        SavedTimes = new List<string>();
        ElapsedTime = TimeSpan.Zero;
    }
    public string? ActualResult { get; set; }
    public IEnumerable<Image>? Evidence { get; set; }
    public List<string> SavedTimes { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan AmountOfTimeSpentToTest { get; set; }
}

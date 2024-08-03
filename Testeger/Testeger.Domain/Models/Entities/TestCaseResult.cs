using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testeger.Domain.Models.Entities;

public class TestCaseResult
{
    public TestCaseResult()
    {
        SavedTimes = new List<string>();
        ElapsedTime = TimeSpan.Zero;
    }

    public string? Id { get; set; }
    public string? TestCaseId { get; set; }
    public string? ActualResult { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan AmountOfTimeSpentToTest { get; set; }
    public bool IsSuccess { get; set; }
    public IEnumerable<string> SavedTimes { get; set; }
    public string SavedTimesJson
    {
        get => JsonSerializer.Serialize(SavedTimes);
        set => SavedTimes = JsonSerializer.Deserialize<ICollection<string>>(value);
    }

    public ICollection<Image>? Images { get; set; }


    [JsonIgnore]
    public TestCase? TestCase { get; set; }
}

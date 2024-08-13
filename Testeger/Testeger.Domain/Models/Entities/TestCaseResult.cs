using System.Text.Json.Serialization;

namespace Testeger.Domain.Models.Entities;

public class TestCaseResult
{
    public TestCaseResult()
    {
        ElapsedTime = TimeSpan.Zero;
    }

    public required string Id { get; set; }
    public int Number { get; set; }
    public required string TestCaseId { get; set; }
    public string? ActualResult { get; set; }
    public TimeSpan? ElapsedTime { get; set; }
    public TimeSpan? AmountOfTimeSpentToTest { get; set; }
    public bool? IsSuccess { get; set; }
    public bool? IsFinished { get; set; }
    public ICollection<Image>? Images { get; set; }


    [JsonIgnore]
    public TestCase? TestCase { get; set; }
}

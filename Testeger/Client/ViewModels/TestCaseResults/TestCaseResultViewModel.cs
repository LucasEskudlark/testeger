namespace Testeger.Client.ViewModels.TestCaseResults;

public class TestCaseResultViewModel
{
    public string? Id { get; set; }
    public int Number { get; set; }
    public string? TestCaseId { get; set; }
    public string? ActualResult { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public bool IsSuccess { get; set; }
}

namespace Testeger.Shared.DTOs.Requests.FinishTestCaseResult;

public class FinishTestCaseResultRequest
{
    public string? Id { get; set; }
    public required string TestCaseId { get; set; }
    public required string ActualResult { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public bool IsSuccess { get; set; }
}

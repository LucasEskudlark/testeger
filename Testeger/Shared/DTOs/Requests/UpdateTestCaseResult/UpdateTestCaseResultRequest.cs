namespace Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;

public class UpdateTestCaseResultRequest
{
    public required string Id { get; set; }
    public required string TestCaseId { get; set; }
    public required string ActualResult { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsFinished { get; set; }
}

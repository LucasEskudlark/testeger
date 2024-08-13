namespace Testeger.Shared.DTOs.Requests.CreateTestCaseResult;

public class CreateTestCaseResultRequest
{
    public required string TestCaseId { get; set; }
    public string? ActualResult { get; set; }
    public TimeSpan? ElapsedTime { get; set; }
    public bool IsSuccess { get; set; }
}

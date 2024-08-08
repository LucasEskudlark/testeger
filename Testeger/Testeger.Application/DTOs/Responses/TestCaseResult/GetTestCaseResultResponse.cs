namespace Testeger.Application.DTOs.Responses.TestCaseResult;

public class GetTestCaseResultResponse
{
    public required string Id { get; set; }
    public int Number { get; set; }
    public required string TestCaseId { get; set; }
    public required string ActualResult { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan AmountOfTimeSpentToTest { get; set; }
    public bool IsSuccess { get; set; }
}

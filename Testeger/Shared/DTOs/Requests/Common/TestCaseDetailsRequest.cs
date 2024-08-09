namespace Testeger.Shared.DTOs.Requests.Common;

public class TestCaseDetailsRequest
{
    public required string Description { get; set; }
    public required string PreConditions { get; set; }
    public required string Steps { get; set; }
    public required string ExpectedResult { get; set; }
    public required string Environment { get; set; }
}

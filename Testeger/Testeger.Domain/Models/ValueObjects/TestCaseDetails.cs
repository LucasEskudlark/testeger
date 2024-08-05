namespace Testeger.Domain.Models.ValueObjects;

public class TestCaseDetails
{
    public string? Description { get; set; }
    public string? PreConditions { get; set; }
    public string? Steps { get; set; }
    public string? ExpectedResult { get; set; }
    public string? Environment { get; set; }
}

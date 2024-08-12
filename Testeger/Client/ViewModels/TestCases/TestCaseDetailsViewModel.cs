namespace Testeger.Client.ViewModels.TestCases;

public class TestCaseDetailsViewModel
{
    public required string Description { get; set; }
    public required string PreConditions { get; set; }
    public required string Steps { get; set; }
    public required string ExpectedResult { get; set; }
    public required string Environment { get; set; }
}

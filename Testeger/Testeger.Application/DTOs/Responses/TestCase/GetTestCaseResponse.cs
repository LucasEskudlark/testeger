using Testeger.Application.DTOs.Enumerations;

namespace Testeger.Application.DTOs.Responses.TestCase;

public class GetTestCaseResponse
{
    public required string Id { get; set; }
    public required string TestRequestId { get; set; }
    public required string ProjectId { get; set; }
    public required string CreatedByUserId { get; set; }
    public required string Title { get; set; }
    public required TestCaseDetailsResponse Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime ScheduledDate { get; set; }
}

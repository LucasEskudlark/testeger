using Testeger.Shared.Models.Enumerations;
using Testeger.Shared.Models.ValueObjects;

namespace Testeger.Shared.Models.Entities;

public class TestCase
{
    public string? Id { get; set; }
    public string? RequestId { get; set; }
    public string? AssignedToUserId { get; set; }
    public string? Title { get; set; }
    public TestCaseDetails? Details { get; set; }
    public TestCaseResults? Results { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
}

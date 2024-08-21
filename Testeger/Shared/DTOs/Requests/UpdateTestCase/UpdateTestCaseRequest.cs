using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Requests.Common;

namespace Testeger.Shared.DTOs.Requests.UpdateTestCase;

public class UpdateTestCaseRequest
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required TestCaseDetailsRequest Details { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime ScheduledDate { get; set; }
}

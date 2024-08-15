using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Shared.DTOs.Responses.TestCase;

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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime ScheduledDate { get; set; }
    public required IEnumerable<TestCaseHistoryResponse> History { get; set; }
    public required IEnumerable<GetTestCaseResultResponse> Results { get; set; }
}

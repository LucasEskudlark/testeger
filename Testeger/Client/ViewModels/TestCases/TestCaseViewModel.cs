using System.Text.Json.Serialization;
using Testeger.Client.ViewModels.TestCaseResults;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Responses.TestCase;

namespace Testeger.Client.ViewModels.TestCases;

public class TestCaseViewModel
{
    public required string Id { get; set; }
    public required string TestRequestId { get; set; }
    public required string ProjectId { get; set; }
    public required string CreatedByUserId { get; set; }
    public required string Title { get; set; }
    public required TestCaseDetailsViewModel Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime ScheduledDate { get; set; }
    public required IEnumerable<TestCaseHistoryViewModel> History { get; set; }
    public required IEnumerable<TestCaseResultViewModel> Results { get; set; }
}

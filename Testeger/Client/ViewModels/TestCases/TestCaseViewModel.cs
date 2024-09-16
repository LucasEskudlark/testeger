using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Testeger.Client.ViewModels.TestCaseResults;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels.TestCases;

public class TestCaseViewModel
{
    public required string Id { get; set; }
    public required string TestRequestId { get; set; }
    public required string ProjectId { get; set; }
    public required string CreatedByUserId { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be at 50 characters at most")]
    public required string Title { get; set; }
    public required TestCaseDetailsViewModel Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    [Required(ErrorMessage = "Status is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus Status { get; set; }
    [Required(ErrorMessage = "Scheduled Date is required")]
    public DateTime ScheduledDate { get; set; }
    public required IEnumerable<TestCaseHistoryViewModel> History { get; set; }
    public required IEnumerable<TestCaseResultViewModel> Results { get; set; }
}

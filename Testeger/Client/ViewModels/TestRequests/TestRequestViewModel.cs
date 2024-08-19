using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels.TestRequests;

public class TestRequestViewModel
{
    public required string Id { get; set; }
    public int Number { get; set; }
    public required string ProjectId { get; set; }
    public required string CreatedByUserId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be at 50 characters at most")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(1500, ErrorMessage = "Title must be at 1500 characters at most")]
    public required string Description { get; set; }

    [Url(ErrorMessage = "Story Link must be a valid URL")]
    public required string StoryLink { get; set; }
    public required string UserAssignedId { get; set; }

    [Required(ErrorMessage = "Request Status is required")]
    [EnumDataType(typeof(RequestStatus))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; }

    [Required(ErrorMessage = "Priority Level is required")]
    [EnumDataType(typeof(PriorityLevel))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PriorityLevel PriorityLevel { get; set; }

    [Required(ErrorMessage = "Due Date is required")]
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public required IEnumerable<TestRequestHistoryViewModel> History { get; set; }
}

using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.DTOs.Requests.UpdateTestRequest;

public class UpdateTestRequestRequest
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string StoryLink { get; set; }
    public string? UserAssignedId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime DueDate { get; set; }
}

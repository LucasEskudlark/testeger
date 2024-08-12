using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.DTOs.Responses.TestRequest;

public class GetTestRequestResponse
{
    public required string Id { get; set; }
    public int Number { get; set; }
    public required string ProjectId { get; set; }
    public required string CreatedByUserId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string StoryLink { get; set; }
    public required string UserAssignedId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public required IEnumerable<TestRequestHistoryResponse> History { get; set; }
}

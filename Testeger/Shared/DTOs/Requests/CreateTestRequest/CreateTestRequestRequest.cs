using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.DTOs.Requests.CreateTestRequest;

public class CreateTestRequestRequest
{
    public required string ProjectId { get; set; }
    public required string UserId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string StoryLink { get; set; }
    public RequestStatus Status { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime DueDate { get; set; }
}

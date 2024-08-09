namespace Testeger.Shared.DTOs.Responses.Project;

public class GetProjectResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string CreatedByUserId { get; set; }
}

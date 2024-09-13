namespace Testeger.Shared.DTOs.Requests.CreateProject;

public class CreateProjectRequest
{
    public required string Name { get; set; }
    public string? UserId { get; set; }
}

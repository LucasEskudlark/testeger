namespace Testeger.Client.ViewModels.Projects;

public class ProjectViewModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string CreatedByUserId { get; set; }
}
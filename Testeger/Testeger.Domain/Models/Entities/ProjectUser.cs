using Testeger.Domain.Models.Entities;

namespace Testeger.Domain.Models.Entities;

public class ProjectUser
{
    public required string ProjectId { get; set; }
    public Project? Project { get; set; }
    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
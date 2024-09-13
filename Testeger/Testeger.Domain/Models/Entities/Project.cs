namespace Testeger.Domain.Models.Entities;

public class Project
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }

    public ICollection<TestRequest>? TestRequests { get; set; }
    public required ICollection<ProjectUser> ProjectUsers { get; set; }
}

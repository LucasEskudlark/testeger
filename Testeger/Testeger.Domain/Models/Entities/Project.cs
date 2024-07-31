namespace Testeger.Domain.Models.Entities;

public class Project
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreationDate { get; set; }
    public string? CreatorId { get; set; }

    public ICollection<User>? Users { get; set; }
}

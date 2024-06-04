namespace Testeger.Shared.Models.Entities;

public class Project
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<User> Members { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatorId { get; set; }
}

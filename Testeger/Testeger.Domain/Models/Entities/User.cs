namespace Testeger.Domain.Models.Entities;

public class User
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreationDate { get; set; }


    public ICollection<Role>? Roles { get; set; }
}

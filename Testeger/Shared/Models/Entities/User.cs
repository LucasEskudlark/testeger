using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Models.Entities;

public class User
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public RoleType Role { get; set; }
}

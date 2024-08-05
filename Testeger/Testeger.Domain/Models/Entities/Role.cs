using Testeger.Domain.Enumerations;

namespace Testeger.Domain.Models.Entities;

public class Role
{
    public string? Id { get; set; }
    public RoleType? Type { get; set; }
    public string? UserId { get; set; }
    public string? ProjectId { get; set; }
}

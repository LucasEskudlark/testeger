using System.Text.Json.Serialization;

namespace Testeger.Domain.Models.Entities;

public class Invitation
{
    public required string Id { get; set; }
    public string? UserId { get; set; }
    public string? ProjectId { get; set; }
    public required string Email { get; set; }
    public required string InvitationToken { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime SentDate { get; set; }

    [JsonIgnore]
    public ApplicationUser? User { get; set; }
    [JsonIgnore]
    public Project? Project { get; set; }
}

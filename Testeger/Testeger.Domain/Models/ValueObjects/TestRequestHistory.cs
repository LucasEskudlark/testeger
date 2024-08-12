using Testeger.Domain.Enumerations;

namespace Testeger.Domain.Models.ValueObjects;

public class TestRequestHistory
{
    public string? ChangedByUserId { get; set; }
    public RequestStatus? OldStatus { get; set; }
    public RequestStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

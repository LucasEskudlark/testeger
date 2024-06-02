using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Models.ValueObjects;

public class TestRequestHistory
{
    public string? TestRequestId { get; set; }
    public string? ChangedByUserId { get; set; }
    public RequestStatus? OldStatus { get; set; }
    public RequestStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

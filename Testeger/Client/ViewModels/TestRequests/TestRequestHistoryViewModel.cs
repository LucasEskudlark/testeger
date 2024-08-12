using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels.TestRequests;

public class TestRequestHistoryViewModel
{
    public required string ChangedByUserId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus OldStatus { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Shared.DTOs.Responses.TestCase;

public class TestCaseHistoryResponse
{
    public required string ChangedByUserId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus OldStatus { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

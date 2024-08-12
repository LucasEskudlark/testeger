using System.Text.Json.Serialization;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels.TestCases;

public class TestCaseHistoryViewModel
{
    public required string ChangedByUserId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus OldStatus { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TestCaseStatus NewStatus { get; set; }
    public DateTime ChangedDate { get; set; }
}

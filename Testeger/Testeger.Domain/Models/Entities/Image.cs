using System.Text.Json.Serialization;

namespace Testeger.Domain.Models.Entities;

public class Image
{
    public string? Id { get; set; }
    public string? TestCaseResultId { get; set; }
    public string? FilePath { get; set; }
    public string? FileName { get; set; }

    [JsonIgnore]
    public TestCaseResult? TestCaseResult { get; set; }
}

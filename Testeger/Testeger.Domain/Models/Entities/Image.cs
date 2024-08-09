using System.Text.Json.Serialization;

namespace Testeger.Domain.Models.Entities;

public class Image
{
    public required string Id { get; set; }
    public required string TestCaseResultId { get; set; }
    public required string FilePath { get; set; }
    public required string FileName { get; set; }

    [JsonIgnore]
    public TestCaseResult? TestCaseResult { get; set; }
}

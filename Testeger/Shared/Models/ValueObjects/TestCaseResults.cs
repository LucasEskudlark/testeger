using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Models.ValueObjects;

public class TestCaseResults
{
    public string? ActualResult { get; set; }
    public IEnumerable<Image>? Evidence { get; set; }

}

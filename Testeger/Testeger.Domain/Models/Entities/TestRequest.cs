using System.Text.Json.Serialization;
using Testeger.Domain.Enumerations;
using Testeger.Domain.Models.ValueObjects;

namespace Testeger.Domain.Models.Entities;

public class TestRequest
{
    public TestRequest()
    {
        TestCases = new List<TestCase>();
        History = new List<TestRequestHistory>();
    }

    public string? Id { get; set; }
    public int Number { get; set; }
    public string? ProjectId { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? StoryLink { get; set; }
    public string? UserAssignedId { get; set; }
    public RequestStatus Status { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }


    public ICollection<TestRequestHistory>? History { get; set; }
    public ICollection<TestCase>? TestCases { get; set; }

    [JsonIgnore]
    public Project Project { get; set; }
}

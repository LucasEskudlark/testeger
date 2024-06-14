using Testeger.Shared.Models.Enumerations;
using Testeger.Shared.Models.ValueObjects;

namespace Testeger.Shared.Models.Entities;

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
    public string? CreatorId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? StoryLink { get; set; }
    public User? AssignedTo { get; set; }
    public RequestStatus Status { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public List<TestRequestHistory> History { get; set; }
    public List<TestCase> TestCases { get; set; }
}

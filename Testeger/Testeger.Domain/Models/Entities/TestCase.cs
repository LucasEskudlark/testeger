﻿using Testeger.Shared.Models.Enumerations;
using Testeger.Shared.Models.ValueObjects;

namespace Testeger.Domain.Models.Entities;

public class TestCase
{
    public TestCase()
    {
        History = new List<TestCaseHistory>();
        Results = new List<TestCaseResult>();
    }

    public string? Id { get; set; }
    public string? RequestId { get; set; }
    public string? ProjectId { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? Title { get; set; }
    public TestCaseDetails? Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public TestCaseStatus Status { get; set; }
    public bool NeedCorrection { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public ICollection<TestCaseHistory> History { get; set; }
    public ICollection<TestCaseResult> Results { get; set; }
    public bool IsCompleted => Status == TestCaseStatus.Completed;

    public TestCaseResult GetLatestTestCaseResult() => Results.Last();
}

﻿using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Requests.Common;

namespace Testeger.Shared.DTOs.Requests.CreateTestCase;

public class CreateTestCaseRequest
{
    public required string TestRequestId { get; set; }
    public required string ProjectId { get; set; }
    public required string UserId { get; set; }
    public required string Title { get; set; }
    public required TestCaseDetailsRequest Details { get; set; }
    public TestCaseStatus Status { get; set; }
}
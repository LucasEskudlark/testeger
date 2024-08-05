using System.ComponentModel;

namespace Testeger.Domain.Enumerations;

public enum TestCaseStatus
{
    [Description("The test case is waiting to be tested.")]
    Pending = 0,

    [Description("The test case has failed during testing.")]
    Failed = 1,

    [Description("The test case has been successfully tested and completed.")]
    Completed = 2,
}

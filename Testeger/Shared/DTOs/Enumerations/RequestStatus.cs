using System.ComponentModel;

namespace Testeger.Shared.DTOs.Enumerations;

public enum RequestStatus
{
    [Description("The request is waiting to be processed.")]
    Waiting = 0,

    [Description("The request is currently being processed.")]
    InProgress = 1,

    [Description("The request is ready to be tested.")]
    ReadyForTesting = 2,

    [Description("The request is being fixed after issues were found.")]
    FixingIssues = 3,

    [Description("The request has been completed.")]
    Completed = 4,

    [Description("None")]
    None = 9999
}

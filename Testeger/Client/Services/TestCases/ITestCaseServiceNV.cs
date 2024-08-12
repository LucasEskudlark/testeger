using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestCase;

namespace Testeger.Client.Services.TestCases;

public interface ITestCaseServiceNV
{
    Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId);
    Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id);

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;
}

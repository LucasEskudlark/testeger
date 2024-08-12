using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Responses.TestCase;

namespace Testeger.Client.Services.TestCases;

public interface ITestCaseServiceNV
{
    Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId);
    Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id);
    Task DeleteTestCaseByIdAsync(string id);
    Task CreateTestCaseAsync(CreateTestCaseRequest request);

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;
}

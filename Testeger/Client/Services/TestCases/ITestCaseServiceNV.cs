using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Requests.CreateTestCase;

namespace Testeger.Client.Services.TestCases;

public interface ITestCaseServiceNV
{
    Task<IEnumerable<TestCaseViewModel>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId);
    Task<TestCaseViewModel> GetTestCaseByIdAsync(string id);
    Task DeleteTestCaseByIdAsync(string id);
    Task CreateTestCaseAsync(CreateTestCaseRequest request);

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;
}

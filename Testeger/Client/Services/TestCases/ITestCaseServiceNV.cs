using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Services.TestCases;

public interface ITestCaseServiceNV
{
    Task<IEnumerable<TestCaseViewModel>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId);
    Task<TestCaseViewModel> GetTestCaseByIdAsync(string id);
    Task DeleteTestCaseByIdAsync(string id);
    Task CreateTestCaseAsync(TestCaseCreationViewModel request);
    Task<IEnumerable<TestCaseViewModel>> GetTestCasesByProjectIdAsync(string projectId);
    Task<Dictionary<TestCaseStatus, IEnumerable<TestCaseViewModel>>> GetTestCasesByProjectIdGroupedByStatus(string projectId);

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;
}

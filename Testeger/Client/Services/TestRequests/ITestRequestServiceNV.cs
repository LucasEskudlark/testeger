using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestRequests;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Services.TestRequests;

public interface ITestRequestServiceNV
{
    public event Action? OnTestRequestAdded;
    public event Action? OnChange;
    public event Action? OnTestRequestDeleted;
    public event Action? OnTestRequestUpdated;

    Task CreateTestRequestAsync(TestRequestCreationViewModel request);
    Task<Dictionary<RequestStatus, IEnumerable<TestRequestViewModel>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId);
    Task<TestRequestViewModel> GetTestRequestByIdAsync(string id);
    Task DeleteTestRequestAsync(string id);
    Task<IEnumerable<TestRequestViewModel>> GetTestRequestsByProjectIdAsync(string projectId);
    Task UpdateTestRequestAsync(TestRequestViewModel request);
}

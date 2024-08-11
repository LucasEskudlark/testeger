using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses.TestRequest;

namespace Testeger.Client.Services.TestRequests;

public interface ITestRequestServiceNV
{
    public event Action? OnTestRequestAdded;
    public event Action? OnChange;
    public event Action? OnTestRequestDeleted;
    public event Action? OnTestRequestUpdated;

    Task CreateTestRequestAsync(CreateTestRequestRequest request);
    Task<Dictionary<RequestStatus, IEnumerable<GetTestRequestResponse>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId);
    Task<GetTestRequestResponse> GetTestRequestByIdAsync(string id);
    Task DeleteTestRequestAsync(string id);
}

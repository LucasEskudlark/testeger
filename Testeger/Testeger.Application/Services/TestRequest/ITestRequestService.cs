using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestRequest;

namespace Testeger.Application.Services.TestRequest;

public interface ITestRequestService
{
    Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request);
    Task<GetTestRequestResponse> GetTestRequestByIdAsync(string id);

    Task<PagedResponse<GetTestRequestResponse>> GetAllTestRequestsPagedAsync(PagedRequest request);
    Task DeleteTestRequestAsync(string id);
    Task<IEnumerable<GetTestRequestResponse>> GetTestRequestsByProjectIdAsync(string projectId);
}

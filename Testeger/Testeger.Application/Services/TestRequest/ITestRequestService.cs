using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.DTOs.Responses.TestRequest;

namespace Testeger.Application.Services.TestRequest;

public interface ITestRequestService
{
    Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request);
    Task<GetTestRequestResponse> GetTestRequestByIdAsync(string id);

    Task<PagedResponse<GetTestRequestResponse>> GetAllTestRequestsPagedAsync(PagedRequest request);
    Task DeleteTestRequestAsync(string id);
}

using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.DTOs.Responses;

namespace Testeger.Application.Services.TestRequest;

public interface ITestRequestService
{
    Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request);
}

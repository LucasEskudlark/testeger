using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestCase;

namespace Testeger.Application.Services.TestCase;

public interface ITestCaseService
{
    Task<CreateTestCaseResponse> CreateTestCaseAsync(CreateTestCaseRequest request);
    Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id);
    Task<PagedResponse<GetTestCaseResponse>> GetAllTestCasesPagedAsync(PagedRequest request);
    Task DeleteTestCaseAsync(string id);
    Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByTestRequestIdAsync(string testRequestId);
    Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByProjectIdAsync(string projectId);
}

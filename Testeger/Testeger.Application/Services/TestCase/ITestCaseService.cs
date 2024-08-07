using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.DTOs.Responses.TestCase;

namespace Testeger.Application.Services.TestCase;

public interface ITestCaseService
{
    Task<CreateTestCaseResponse> CreateTestCaseAsync(CreateTestCaseRequest request);
    Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id);
    Task<PagedResponse<GetTestCaseResponse>> GetAllTestCasesPagedAsync(PagedRequest request);
}

using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.DTOs.Responses.TestCaseResult;

namespace Testeger.Application.Services.TestCaseResult;

public interface ITestCaseResultService
{
    Task<CreateTestCaseResultResponse> CreateTestCaseResultAsync(CreateTestCaseResultRequest request);
    Task<PagedResponse<GetTestCaseResultResponse>> GetAllTestCaseResultsPagedAsync(PagedRequest request);
    Task<GetTestCaseResultResponse> GetTestCaseResultByIdAsync(string id);
}

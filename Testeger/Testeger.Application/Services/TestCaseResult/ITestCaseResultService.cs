using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.FinishTestCaseResult;
using Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Application.Services.TestCaseResult;

public interface ITestCaseResultService
{
    Task<CreateTestCaseResultResponse> CreateTestCaseResultAsync(CreateTestCaseResultRequest request);
    Task<PagedResponse<GetTestCaseResultResponse>> GetAllTestCaseResultsPagedAsync(PagedRequest request);
    Task<GetTestCaseResultResponse> GetTestCaseResultByIdAsync(string id);
    Task<IEnumerable<GetTestCaseResultResponse>> GetResultsByTestCaseIdAsync(string testCaseId);
    Task UpdateTestCaseResultAsync(UpdateTestCaseResultRequest request);
    Task<CreateTestCaseResultResponse> FinishTestCaseResultAsync(FinishTestCaseResultRequest request);
}

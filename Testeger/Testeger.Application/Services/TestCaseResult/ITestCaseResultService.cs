using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.DTOs.Responses.TestCaseResult;

namespace Testeger.Application.Services.TestCaseResult;

public interface ITestCaseResultService
{
    Task<CreateTestCaseResultResponse> CreateTestCaseResultAsync(CreateTestCaseResultRequest request);
}

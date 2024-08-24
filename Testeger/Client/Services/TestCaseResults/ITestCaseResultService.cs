using Testeger.Client.ViewModels.TestCaseResults;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Client.Services.TestCaseResults;

public interface ITestCaseResultService
{
    Task<IEnumerable<TestCaseResultViewModel>> GetResultsByTestCaseIdAsync(string testCaseId);
    Task<TestCaseResultViewModel> GetLastResultOrDefaultForTestCaseAsync(string testCaseId);
    Task<CreateTestCaseResultResponse> HandleTestFinished(TestCaseResultViewModel viewModel);
}

using Testeger.Client.ViewModels.TestCaseResults;

namespace Testeger.Client.Services.TestCaseResults;

public interface ITestCaseResultService
{
    Task<IEnumerable<TestCaseResultViewModel>> GetResultsByTestCaseIdAsync(string testCaseId);
    Task<TestCaseResultViewModel> GetLastResultOrDefaultForTestCaseAsync(string testCaseId);
}

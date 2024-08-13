using System.Net.Http.Json;
using Testeger.Client.ViewModels.TestCaseResults;

namespace Testeger.Client.Services.TestCaseResults;

public class TestCaseResultService : BaseService, ITestCaseResultService
{
    private const string BaseAddress = "api/testcaseresults";

    public TestCaseResultService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<TestCaseResultViewModel>> GetResultsByTestCaseIdAsync(string testCaseId)
    {
        var address = BaseAddress + $"/testcase/{testCaseId}";

        var response = await _httpClient.GetFromJsonAsync<IEnumerable<TestCaseResultViewModel>>(address);

        return response;
    }

    public async Task<TestCaseResultViewModel> GetLastResultOrDefaultForTestCaseAsync(string testCaseId)
    {
        var testCaseResults = await GetResultsByTestCaseIdAsync(testCaseId);

        if (!testCaseResults.Any())
        {
            return new TestCaseResultViewModel();
        }

        var lastTestCaseResult = testCaseResults.LastOrDefault() ?? new TestCaseResultViewModel();

        return lastTestCaseResult;
    }
}

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
        var nonFinishedTestCaseResults = testCaseResults.Where(tr => tr.IsFinished == false);

        if (!nonFinishedTestCaseResults.Any())
        {
            return new TestCaseResultViewModel(testCaseId);
        }

        var lastTestCaseResult = nonFinishedTestCaseResults.LastOrDefault() ?? new TestCaseResultViewModel(testCaseId);

        return lastTestCaseResult;
    }

    public async Task HandleTestFinished(TestCaseResultViewModel viewModel)
    {
        var address = BaseAddress + $"/finish";
        var response = await _httpClient.PostAsJsonAsync(address, viewModel);
    }
}

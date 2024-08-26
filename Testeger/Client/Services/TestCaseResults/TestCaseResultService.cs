using System.Net.Http.Json;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels.TestCaseResults;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Client.Services.TestCaseResults;

public class TestCaseResultService : BaseService, ITestCaseResultService
{
    private const string BaseAddress = "api/testcaseresults";

    public TestCaseResultService(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService)
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

    public async Task<CreateTestCaseResultResponse> HandleTestFinished(TestCaseResultViewModel viewModel)
    {
        var address = BaseAddress + $"/finish";
        var response = await _httpClient.PostAsJsonAsync(address, viewModel);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not finish the test.");
            return null;
        }

        _notificationService.ShowSuccessNotification("Success", "Test successfully finished.");

        var creationResponse = await response.Content.ReadFromJsonAsync<CreateTestCaseResultResponse>();
        return creationResponse;
    }
}

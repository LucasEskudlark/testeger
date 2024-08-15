using System.Net.Http.Json;
using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Services.TestCases;

public class TestCaseServiceNV : BaseService, ITestCaseServiceNV
{
    private const string BaseAddress = "api/testcases";
    public TestCaseServiceNV(HttpClient httpClient) : base(httpClient)
    {
    }

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;

    public async Task<IEnumerable<TestCaseViewModel>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId)
    {
        var address = BaseAddress + $"/request/{testRequestId}";
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<TestCaseViewModel>>(address);

        return response;
    }

    public async Task<TestCaseViewModel> GetTestCaseByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<TestCaseViewModel>(address);

        return response;
    }

    public async Task DeleteTestCaseByIdAsync(string id)
    {
        var address = BaseAddress + $"/delete/{id}";
        await _httpClient.PostAsJsonAsync(address, id);
        OnTestCaseDeleted?.Invoke();
    }

    public async Task CreateTestCaseAsync(TestCaseCreationViewModel request)
    {
        await _httpClient.PostAsJsonAsync(BaseAddress, request);
        OnTestCaseAdded?.Invoke();
    }

    public async Task<IEnumerable<TestCaseViewModel>> GetTestCasesByProjectIdAsync(string projectId)
    {
        var address = BaseAddress + $"/project/{projectId}";
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<TestCaseViewModel>>(address);

        return response;
    }

    public async Task<Dictionary<TestCaseStatus, IEnumerable<TestCaseViewModel>>> GetTestCasesByProjectIdGroupedByStatus(string projectId)
    {
        var testCases = await GetTestCasesByProjectIdAsync(projectId);

        return Enum.GetValues(typeof(TestCaseStatus)).Cast<TestCaseStatus>()
            .ToDictionary(
                status => status,
                status => testCases.Where(tr => tr.Status == status));
    }
}

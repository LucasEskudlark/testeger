using System.Net.Http.Json;
using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Requests.CreateTestCase;

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

    public async Task CreateTestCaseAsync(CreateTestCaseRequest request)
    {
        await _httpClient.PostAsJsonAsync(BaseAddress, request);
        OnTestCaseAdded?.Invoke();
    }
}

using System.Net.Http.Json;
using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestRequests;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Responses;

namespace Testeger.Client.Services.TestRequests;

public class TestRequestServiceNV : BaseService, ITestRequestServiceNV
{
    private const string BaseAddress = "api/testrequests";

    public TestRequestServiceNV(HttpClient httpClient) : base(httpClient)
    {
    }

    public event Action? OnTestRequestAdded;
    public event Action? OnChange;
    public event Action? OnTestRequestDeleted;
    public event Action? OnTestRequestUpdated;

    public async Task CreateTestRequestAsync(TestRequestCreationViewModel request)
    {
        await _httpClient.PostAsJsonAsync(BaseAddress, request);
        OnTestRequestAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task DeleteTestRequestAsync(string id)
    {
        var address = BaseAddress + $"/delete/{id}";
        await _httpClient.PostAsJsonAsync(address, id);
        OnTestRequestDeleted?.Invoke();
        NotifyStateChanged();
    }

    public async Task<PagedResponse<TestRequestViewModel>> GetAllTestRequestsPagedAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<PagedResponse<TestRequestViewModel>>(BaseAddress);

        return response;
    }

    public async Task<TestRequestViewModel> GetTestRequestByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<TestRequestViewModel>(address);

        return response;
    }

    public async Task<Dictionary<RequestStatus, IEnumerable<TestRequestViewModel>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId)
    {
        var testRequests = await GetAllTestRequestsPagedAsync();

        return Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>()
            .ToDictionary(
                status => status,
                status => testRequests.Items.Where(tr => tr.Status == status && tr.ProjectId == projectId));
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

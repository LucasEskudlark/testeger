using System.Net.Http.Json;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestRequests;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Responses;

namespace Testeger.Client.Services.TestRequests;

public class TestRequestServiceNV : BaseService, ITestRequestServiceNV
{
    private const string BaseAddress = "api/testrequests";

    public TestRequestServiceNV(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService)
    {
    }

    public event Action? OnTestRequestAdded;
    public event Action? OnChange;
    public event Action? OnTestRequestDeleted;
    public event Action? OnTestRequestUpdated;

    public async Task CreateTestRequestAsync(TestRequestCreationViewModel request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseAddress, request);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not create test request.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Test request successfully created.");
        OnTestRequestAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task DeleteTestRequestAsync(string id)
    {
        var address = BaseAddress + $"/delete/{id}";
        var response = await _httpClient.PostAsJsonAsync(address, id);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not delete test request.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Test request successfully deleted.");
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

    public async Task<IEnumerable<TestRequestViewModel>> GetTestRequestsByProjectIdAsync(string projectId)
    {
        var address = BaseAddress + $"/project/{projectId}";
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<TestRequestViewModel>>(address);

        return response;
    }

    public async Task<Dictionary<RequestStatus, IEnumerable<TestRequestViewModel>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId)
    {
        var testRequests = await GetTestRequestsByProjectIdAsync(projectId);

        return Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>()
            .Where(status => status != RequestStatus.None)
            .ToDictionary(
                status => status,
                status => testRequests.Where(tr => tr.Status == status));
    }

    public async Task UpdateTestRequestAsync(TestRequestViewModel request)
    {
        var address = BaseAddress + $"/update";
        var response = await _httpClient.PostAsJsonAsync(address, request);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

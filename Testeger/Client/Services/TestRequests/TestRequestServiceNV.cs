using System.Net.Http.Json;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestRequest;

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

    public async Task CreateTestRequestAsync(CreateTestRequestRequest request)
    {
        await _httpClient.PostAsJsonAsync(BaseAddress, request);
        OnTestRequestAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task<PagedResponse<GetTestRequestResponse>> GetAllTestRequestsPagedAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<PagedResponse<GetTestRequestResponse>>(BaseAddress);

        return response;
    }

    public async Task<Dictionary<RequestStatus, IEnumerable<GetTestRequestResponse>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId)
    {
        var testRequests = await GetAllTestRequestsPagedAsync();

        return Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>()
            .ToDictionary(
                status => status,
                status => testRequests.Items.Where(tr => tr.Status == status && tr.ProjectId == projectId));
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

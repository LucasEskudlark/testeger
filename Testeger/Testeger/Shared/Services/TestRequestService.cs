using Blazored.LocalStorage;
using Newtonsoft.Json;
using Testeger.Shared.Models.Entities;
using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Services;

public class TestRequestService
{
    private readonly ILocalStorageService _localStorage;
    private const string StorageKey = "testRequests";
    public event Action? OnChange;

    public TestRequestService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<List<TestRequest>> GetTestRequestsByProjectIdAndStatus(string id, RequestStatus status)
    {
        var testRequests = await GetAllTestRequests();
        return testRequests.Where(tr => tr.Status == status && tr.ProjectId == id).ToList();
    }

    public async Task<List<TestRequest>> GetAllTestRequests()
    {
        var json = await _localStorage.GetItemAsStringAsync(StorageKey);

        if (string.IsNullOrEmpty(json))
        {
            return new List<TestRequest>();
        }

        var innerJson = JsonConvert.DeserializeObject<string>(json);
        return JsonConvert.DeserializeObject<List<TestRequest>>(innerJson) ?? new List<TestRequest>();
    }

    public async Task AddTestRequest(TestRequest testRequest)
    {
        var testRequests = await GetAllTestRequests();
        var existingRequest = testRequests.FirstOrDefault(tr => tr.Id == testRequest.Id);
        if (existingRequest != null)
        {
            testRequests.Remove(existingRequest);
        }
        testRequests.Add(testRequest);

        var jsonString = JsonConvert.SerializeObject(testRequests);
        await _localStorage.SetItemAsync(StorageKey, jsonString);
        NotifyStateChanged();
    }

    public async Task ClearStorage()
    {
        await _localStorage.ClearAsync();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

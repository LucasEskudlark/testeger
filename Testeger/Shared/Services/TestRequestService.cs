using Blazored.LocalStorage;
using Newtonsoft.Json;
using Testeger.Shared.Exceptions;
using Testeger.Shared.Models.Entities;
using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Services;

public class TestRequestService
{
    private readonly LocalStorageService _localStorageService;
    private const string StorageKey = "testRequests";
    public event Action? OnChange;
    public event Action? OnTestRequestAdded;
    public event Action? OnTestRequestDeleted;
    public event Action? OnTestRequestUpdated;

    public TestRequestService(LocalStorageService localStorage)
    {
        _localStorageService = localStorage;
    }

    public async Task<List<TestRequest>> GetTestRequestsByProjectIdAndStatus(string id, RequestStatus status)
    {
        var testRequests = await GetAllTestRequests();
        return testRequests.Where(tr => tr.Status == status && tr.ProjectId == id).ToList();
    }

    public async Task<List<TestRequest>> GetTestRequestsByProjectId(string id)
    {
        var testRequests = await GetAllTestRequests();
        return testRequests.Where(tr => tr.ProjectId == id).ToList();
    }

    public async Task<TestRequest> GetTestRequestById(string id)
    {
        var testRequests = await GetAllTestRequests();
        
        return testRequests.Find(tr => tr.Id == id) ?? throw new TestRequestNotFoundException($"Test Request with id {id} was not found.");
    }

    public async Task<List<TestRequest>> GetAllTestRequests()
    {
        var testRequests = await _localStorageService.ReadFromStorage<List<TestRequest>>(StorageKey);
        return testRequests ?? new List<TestRequest>();
    }

    public async Task<Dictionary<RequestStatus, List<TestRequest>>> GetTestRequestsByProjectIdGroupedByStatus(string projectId)
    {
        var testRequests = await GetAllTestRequests();
        return Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>()
                   .ToDictionary(
                       status => status,
                       status => testRequests.Where(tr => tr.Status == status && tr.ProjectId == projectId).ToList());
    }

    public async Task<int> GetNextTestRequestNumber(string projectId)
    {
        var testRequests = await GetAllTestRequests();
        var projectRequests = testRequests.Where(tr => tr.ProjectId == projectId).ToList();

        if (!projectRequests.Any())
        {
            return 1;
        }

        return projectRequests.Max(tr => tr.Number) + 1;
    }


    public async Task AddTestRequest(TestRequest testRequest)
    {
        testRequest.Number = await GetNextTestRequestNumber(testRequest.ProjectId);

        Console.WriteLine($"Adding Test Request: {JsonConvert.SerializeObject(testRequest)}");

        var testRequests = await GetAllTestRequests();

        Console.WriteLine($"Existing requests: {JsonConvert.SerializeObject(testRequests)}");

        if (testRequests.Contains(testRequest))
        {
            testRequests.Remove(testRequest);
        }
        testRequests.Add(testRequest);

        await _localStorageService.WriteToStorage(StorageKey, testRequests);
        OnTestRequestAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task RemoveTestRequest(TestRequest testRequest)
    {
        var testRequests = await GetAllTestRequests();

        var testRequestToRemove = testRequests.Find(tr => tr.Id == testRequest.Id);

        if (testRequestToRemove != null)
        {
            testRequests.Remove(testRequestToRemove);
        }

        await _localStorageService.WriteToStorage(StorageKey, testRequests);
        OnTestRequestDeleted?.Invoke();
        NotifyStateChanged();
    }

    public async Task UpdateTestRequest(TestRequest testRequest)
    {
        var testRequests = await GetAllTestRequests();
        var index = testRequests.FindIndex(tr => tr.Id == testRequest.Id);

        if (index == -1)
        {
            throw new TestRequestNotFoundException($"Test Request with id {testRequest.Id} was not found.");
        }

        testRequests[index] = testRequest;
        await _localStorageService.WriteToStorage(StorageKey, testRequests);
        OnTestRequestUpdated?.Invoke();
        NotifyStateChanged();
    }

    public async Task ClearStorage()
    {
        await _localStorageService.ClearStorage();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

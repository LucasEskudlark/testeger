using Blazored.LocalStorage;
using Newtonsoft.Json;
using Testeger.Shared.Exceptions;
using Testeger.Shared.Models.Entities;
using Testeger.Shared.Models.Enumerations;

namespace Testeger.Shared.Services;

public class TestCaseService
{
    private readonly ILocalStorageService _localStorage;
    private const string StorageKey = "testCases";

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCasetDeleted;

    public TestCaseService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<List<TestCase>> GetAllTestCases()
    {
        var json = await _localStorage.GetItemAsStringAsync(StorageKey);

        if (string.IsNullOrEmpty(json))
        {
            return new List<TestCase>();
        }

        var innerJson = JsonConvert.DeserializeObject<string>(json);
        return JsonConvert.DeserializeObject<List<TestCase>>(innerJson) ?? new List<TestCase>();
    }

    public async Task<TestCase> GetTestCaseById(string id)
    {
        var testCases = await GetAllTestCases();

        return testCases.Find(tc => tc.Id == id) ?? throw new TestCaseNotFoundException($"Test Case with id {id} was not found.");
    }

    public async Task<List<TestCase>> GetTestCasesByRequestId(string requestId)
    {
        var testCases = await GetAllTestCases();
        return testCases.Where(tc => tc.RequestId == requestId).ToList();
    }

    public async Task AddTestCase(TestCase testCase)
    {
        var testCases = await GetAllTestCases();

        if (testCases.Contains(testCase))
        {
            testCases.Remove(testCase);
        }

        testCases.Add(testCase);

        var jsonString = JsonConvert.SerializeObject(testCases);
        await _localStorage.SetItemAsync(StorageKey, jsonString);
        OnTestCaseAdded?.Invoke();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

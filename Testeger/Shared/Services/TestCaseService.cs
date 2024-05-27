using Blazored.LocalStorage;
using Newtonsoft.Json;
using Testeger.Shared.Exceptions;
using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Services;

public class TestCaseService
{
    private readonly LocalStorageService _localStorageService;
    private const string StorageKey = "testCases";

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;

    public TestCaseService(LocalStorageService localStorage)
    {
        _localStorageService = localStorage;
    }

    public async Task<List<TestCase>> GetAllTestCases()
    {
        var testRequests = await _localStorageService.ReadFromStorage<List<TestCase>>(StorageKey);
        return testRequests ?? new List<TestCase>();
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

        await _localStorageService.WriteToStorage(StorageKey, testCases);
        OnTestCaseAdded?.Invoke();
        NotifyStateChanged();
    }

    public async Task RemoveTestCase(TestCase testCase)
    {
        var testCases = await GetAllTestCases();

        var testCaseToRemove = testCases.Find(tc => tc.Id == testCase.Id);

        if (testCaseToRemove != null)
        {
            testCases.Remove(testCaseToRemove);
        }

        await _localStorageService.WriteToStorage(StorageKey, testCases);
        OnTestCaseDeleted?.Invoke();
        NotifyStateChanged();
    }

    public async Task UpdateTestCase(TestCase testCase)
    {
        var testCases = await GetAllTestCases();
        var index = testCases.FindIndex(tr => tr.Id == testCase.Id);

        if (index == -1)
        {
            throw new TestCaseNotFoundException($"Test Case with id {testCase.Id} was not found.");
        }

        testCases[index] = testCase;
        await _localStorageService.WriteToStorage(StorageKey, testCases);
        OnTestCaseUpdated?.Invoke();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

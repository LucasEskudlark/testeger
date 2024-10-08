﻿using System.Net.Http.Json;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Services.TestCases;

public class TestCaseServiceNV : BaseService, ITestCaseServiceNV
{
    private const string BaseAddress = "api/testcases";
    public TestCaseServiceNV(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService)
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
        var response = await _httpClient.PostAsJsonAsync(address, id);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not delete test case.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Test case successfully deleted.");
        OnTestCaseDeleted?.Invoke();
    }

    public async Task CreateTestCaseAsync(TestCaseCreationViewModel request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseAddress, request);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not create test case.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Test case successfully created.");
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
            .Where(status => status != TestCaseStatus.None)
            .ToDictionary(
                status => status,
                status => testCases.Where(tr => tr.Status == status));
    }

    public async Task UpdateTestCaseAsync(TestCaseViewModel testCaseViewModel)
    {
        var address = BaseAddress + "/update";
        var response = await _httpClient.PostAsJsonAsync(address, testCaseViewModel);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not update test case.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Test case successfully updated.");
        OnTestCaseUpdated?.Invoke();
    }
}

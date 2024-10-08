﻿using System.Net.Http.Json;
using Testeger.Client.Services.Authentication;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.Projects;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public class ProjectServiceNV : BaseService, IProjectServiceNV
{
    private readonly ICustomAuthService _authService;
    private const string BaseAddress = "api/projects";

    public ProjectServiceNV(
        HttpClient httpClient,
        INotificationService notificationService,
        ICustomAuthService authService)
        : base(httpClient, notificationService)
    {
        _authService = authService;
    }

    public event Action? OnChange;
    public event Action? OnProjectAdded;
    public event Action? OnProjectDeleted;
    public event Action? OnProjectUpdated;

    public async Task<CreateProjectResponse> CreateProjectAsync(ProjectCreationViewModel request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseAddress, request);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not create project.");
        }

        var creationResponse = await response.Content.ReadFromJsonAsync<CreateProjectResponse>();
        _notificationService.ShowSuccessNotification("Success", "Project successfully created.");

        await _authService.ReAuthenticateUserAsync();

        OnProjectAdded?.Invoke();
        NotifyStateChanged();

        return creationResponse;
    }

    public async Task<PagedResponse<ProjectViewModel>> GetAllProjectsPagedAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<PagedResponse<ProjectViewModel>>(BaseAddress);

        return response;
    }

    public async Task<ProjectViewModel> GetProjectByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<ProjectViewModel>(address);

        return response;
    }

    public async Task<IEnumerable<ProjectViewModel>> GetProjectsForUserAsync()
    {
        var address = BaseAddress + $"/user";
        
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectViewModel>>(address);

        return response;
    }

    public async Task DeleteProjectAsync(string id)
    {
        var address = BaseAddress + $"/delete/{id}";

        var response = await _httpClient.PostAsJsonAsync(address, id);

        if (!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not delete project.");
        }

        _notificationService.ShowSuccessNotification("Success", "Project successfully deleted.");
        OnProjectDeleted?.Invoke();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

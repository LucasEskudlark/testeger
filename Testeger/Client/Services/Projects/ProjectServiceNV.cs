using System.Net;
using System.Net.Http.Json;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public class ProjectServiceNV : BaseService, IProjectServiceNV
{
    private const string BaseAddress = "api/projects";

    public ProjectServiceNV(HttpClient httpClient) : base(httpClient)
    {
    }

    public event Action? OnChange;
    public event Action? OnProjectAdded;
    public event Action? OnProjectDeleted;
    public event Action? OnProjectUpdated;

    public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseAddress, request);

        var creationResponse = await response.Content.ReadFromJsonAsync<CreateProjectResponse>();
        OnProjectAdded?.Invoke();
        NotifyStateChanged();

        return creationResponse;
    }

    public async Task<PagedResponse<GetProjectResponse>> GetAllProjectsPagedAsync()
    {
        var response =  await _httpClient.GetFromJsonAsync<PagedResponse<GetProjectResponse>>(BaseAddress);

        return response;
    }

    public async Task<GetProjectResponse> GetProjectByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<GetProjectResponse>(address);

        return response;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

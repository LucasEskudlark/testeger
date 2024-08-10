using System.Net.Http.Json;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public class ProjectServiceNV : BaseService, IProjectServiceNV
{
    private const string BaseAddress = "api/projects";

    public ProjectServiceNV(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseAddress, request);

        var creationResponse = await response.Content.ReadFromJsonAsync<CreateProjectResponse>();

        return creationResponse;
    }

    public async Task<GetProjectResponse> GetProjectByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<GetProjectResponse>(address);

        return response;
    }
}

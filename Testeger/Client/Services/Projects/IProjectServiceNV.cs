using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public interface IProjectServiceNV
{
    public event Action? OnChange;
    public event Action? OnProjectAdded;
    public event Action? OnProjectDeleted;
    public event Action? OnProjectUpdated;
    Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request);
    Task<GetProjectResponse> GetProjectByIdAsync(string id);
    Task<PagedResponse<GetProjectResponse>> GetAllProjectsPagedAsync();
}

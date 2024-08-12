using Testeger.Client.ViewModels;
using Testeger.Client.ViewModels.Projects;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public interface IProjectServiceNV
{
    public event Action? OnChange;
    public event Action? OnProjectAdded;
    public event Action? OnProjectDeleted;
    public event Action? OnProjectUpdated;
    Task<CreateProjectResponse> CreateProjectAsync(ProjectCreationViewModel request);
    Task<ProjectViewModel> GetProjectByIdAsync(string id);
    Task<PagedResponse<ProjectViewModel>> GetAllProjectsPagedAsync();
}

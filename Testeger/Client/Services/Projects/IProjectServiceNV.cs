using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Client.Services.Projects;

public interface IProjectServiceNV
{
    Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request);
    Task<GetProjectResponse> GetProjectByIdAsync(string id);
}

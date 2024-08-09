using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;
namespace Testeger.Application.Services.Project;

public interface IProjectService
{
    Task<CreateProjectResponse> CreateProject(CreateProjectRequest request);
    Task<GetProjectResponse> GetProjectById(string id);
    Task<PagedResponse<GetProjectResponse>> GetAllProjectsAsync(PagedRequest request);
    Task DeleteProject(string id);
}

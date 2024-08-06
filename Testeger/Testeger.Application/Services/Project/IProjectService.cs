using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.DTOs.Responses.Project;

namespace Testeger.Application.Services.Project;

public interface IProjectService
{
    Task<CreateProjectResponse> CreateProject(CreateProjectRequest request);
    Task<GetProjectResponse> GetProjectById(string id);
    Task<PagedResponse<GetProjectResponse>> GetAllProjectsAsync(PagedRequest request);
    Task DeleteProject(string id);
}

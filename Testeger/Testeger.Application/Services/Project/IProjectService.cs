using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Responses;

namespace Testeger.Application.Services.Project;

public interface IProjectService
{
    Task<CreateProjectResponse> CreateProject(CreateProjectRequest request);
    Task<GetProjectResponse> GetProjectById(string id);
}

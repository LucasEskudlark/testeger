using Testeger.Application.Requests.CreateProject;
using Testeger.Application.Responses;

namespace Testeger.Application.Services.Project;

public interface IProjectService
{
    Task<CreateProjectResponse> CreateProject(CreateProjectRequest request);
}

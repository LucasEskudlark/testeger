using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Requests.CreateProject;
using Testeger.Application.Services.Project;

namespace Testeger.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody]CreateProjectRequest request)
    {
        var response = await _projectService.CreateProject(request);

        return Ok(response);
    }
}

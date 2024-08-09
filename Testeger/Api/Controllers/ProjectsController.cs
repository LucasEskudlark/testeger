using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.Project;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateProject;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectRequest request)
    {
        var response = await _projectService.CreateProject(request);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectByIdAsync(string id)
    {
        var response = await _projectService.GetProjectById(id);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjectsAsync([FromQuery] PagedRequest request)
    {
        var response = await _projectService.GetAllProjectsAsync(request);

        return Ok(response);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteProjectAsync(string id)
    {
        await _projectService.DeleteProject(id);

        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Extensions;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Project;
using Testeger.Shared.Authorization;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateProject;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ICustomAuthenticationService _authService;

    public ProjectsController(IProjectService projectService, ICustomAuthenticationService authService)
    {
        _projectService = projectService;
        _authService = authService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectRequest request)
    {
        var userId = User.GetUserId();
        request.UserId = userId;

        var response = await _projectService.CreateProject(request);

        await _projectService.AddUserToProjectAsync(response.Id, userId);

        await _authService.AddUserToProjectRoleAsync(
            User.GetUserId(),
            AuthorizationRoles.GetRoleForProject(response.Id, AuthorizationRoles.Manager));

        await _authService.CreateRoleAsync(AuthorizationRoles.GetRoleForProject(response.Id, AuthorizationRoles.QA));
        await _authService.CreateRoleAsync(AuthorizationRoles.GetRoleForProject(response.Id, AuthorizationRoles.Developer));

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectByIdAsync(string id)
    {
        var response = await _projectService.GetProjectById(id);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllProjectsAsync([FromQuery] PagedRequest request)
    {
        var response = await _projectService.GetAllProjectsAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetProjectsForUserAsync(string? requestUserId)
    {
        var userId = string.IsNullOrEmpty(requestUserId) ? User.GetUserId() : requestUserId;

        var response = await _projectService.GetProjectsForUserAsync(userId);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteProjectAsync(string id)
    {
        await _projectService.DeleteProject(id);

        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Testeger.Application.Helpers;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Project;
using Testeger.Client.Services.Authentication;
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

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectRequest request)
    {
        var userId = UserHelper.GetUserId(User);
        request.UserId = userId;

        var response = await _projectService.CreateProject(request);
        await _projectService.AddUserToProjectAsync(response.Id, userId);

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

    [HttpGet("user")]
    public async Task<IActionResult> GetProjectsForUserAsync(string? requestUserId)
    {
        var userId = string.IsNullOrEmpty(requestUserId) ? UserHelper.GetUserId(User) : requestUserId;

        var response = await _projectService.GetProjectsForUserAsync(userId);

        return Ok(response);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteProjectAsync(string id)
    {
        await _projectService.DeleteProject(id);

        return NoContent();
    }
}

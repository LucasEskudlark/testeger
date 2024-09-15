using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Extensions;
using Testeger.Application.Services.TestRequest;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Requests.UpdateTestRequest;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestRequestsController : ControllerBase
{
    private readonly ITestRequestService _testRequestService;

    public TestRequestsController(ITestRequestService testRequestService)
    {
        _testRequestService = testRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateTestRequestRequest request)
    {
        request.UserId = User.GetUserId();
        var response = await _testRequestService.CreateTestRequestAsync(request);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestRequestByIdAsync(string id)
    {
        var response = await _testRequestService.GetTestRequestByIdAsync(id);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTestRequestsAsync([FromQuery] PagedRequest request)
    {
        var response = await _testRequestService.GetAllTestRequestsPagedAsync(request);

        return Ok(response);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteTestRequestAsync(string id)
    {
        await _testRequestService.DeleteTestRequestAsync(id);

        return NoContent();
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GeTestRequestsByProjectIdAsync([FromRoute] string projectId)
    {
        var response = await _testRequestService.GetTestRequestsByProjectIdAsync(projectId);

        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateTestRequestAsync([FromBody] UpdateTestRequestRequest request)
    {
        await _testRequestService.UpdateTestRequestAsync(request);

        return NoContent();
    }
}

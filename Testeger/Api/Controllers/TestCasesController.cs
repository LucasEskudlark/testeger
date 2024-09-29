using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Extensions;
using Testeger.Application.Services.TestCase;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.UpdateTestCase;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestCasesController : ControllerBase
{
    private readonly ITestCaseService _testCaseService;

    public TestCasesController(ITestCaseService testCaseService)
    {
        _testCaseService = testCaseService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTestCaseAsync([FromBody] CreateTestCaseRequest request)
    {
        request.UserId = User.GetUserId();
        var response = await _testCaseService.CreateTestCaseAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestCaseByIdAsync([FromRoute] string id)
    {
        var response = await _testCaseService.GetTestCaseByIdAsync(id);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllTestCasesPagedAsync([FromQuery] PagedRequest request)
    {
        var response = await _testCaseService.GetAllTestCasesPagedAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteTestCaseAsync([FromRoute] string id)
    {
        await _testCaseService.DeleteTestCaseAsync(id);

        return NoContent();
    }

    [Authorize]
    [HttpGet("request/{testRequestId}")]
    public async Task<IActionResult> GetTestCasesByTestRequestIdAsync([FromRoute] string testRequestId)
    {
        var response = await _testCaseService.GetTestCasesByTestRequestIdAsync(testRequestId);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GeTestRequestsByProjectIdAsync(string projectId)
    {
        var response = await _testCaseService.GetTestCasesByProjectIdAsync(projectId);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateTestCaseAsync([FromBody] UpdateTestCaseRequest request)
    {
        await _testCaseService.UpdateTestCaseAsync(request);

        return NoContent();
    }
}


using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.TestCaseResult;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.FinishTestCaseResult;
using Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestCaseResultsController : ControllerBase
{
    private readonly ITestCaseResultService _testCaseResultService;

    public TestCaseResultsController(ITestCaseResultService testCaseResultService)
    {
        _testCaseResultService = testCaseResultService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestCaseResultAsync([FromBody] CreateTestCaseResultRequest request)
    {
        var response = await _testCaseResultService.CreateTestCaseResultAsync(request);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTestCaseResultsPagedAsync([FromQuery] PagedRequest request)
    {
        var response = await _testCaseResultService.GetAllTestCaseResultsPagedAsync(request);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestCaseResultByIdAsync(string id)
    {
        var response = await _testCaseResultService.GetTestCaseResultByIdAsync(id);

        return Ok(response);
    }

    [HttpGet("testcase/{testCaseId}")]
    public async Task<IActionResult> GetResultsByTestCaseIdAsync([FromRoute] string testCaseId)
    {
        var response = await _testCaseResultService.GetResultsByTestCaseIdAsync(testCaseId);

        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateTestCaseResultAsync([FromBody] UpdateTestCaseResultRequest request)
    {
        await _testCaseResultService.UpdateTestCaseResultAsync(request);

        return NoContent();
    }

    [HttpPost("finish")]
    public async Task<IActionResult> FinishTestCaseResultAsync([FromBody] FinishTestCaseResultRequest request)
    {
        var response = await _testCaseResultService.FinishTestCaseResultAsync(request);

        return Ok(response);
    }
}

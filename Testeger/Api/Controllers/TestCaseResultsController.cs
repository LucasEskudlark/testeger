using Microsoft.AspNetCore.Mvc;
using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.Services.TestCaseResult;

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
}

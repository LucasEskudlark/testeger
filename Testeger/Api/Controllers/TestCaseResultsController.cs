using Microsoft.AspNetCore.Mvc;
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
}

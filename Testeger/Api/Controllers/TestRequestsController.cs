using Microsoft.AspNetCore.Mvc;
using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.Services.TestRequest;

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
        var response = await _testRequestService.CreateTestRequestAsync(request);

        return Ok(response);
    }
}

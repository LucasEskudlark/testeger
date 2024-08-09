using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.TestRequest;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;

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
}

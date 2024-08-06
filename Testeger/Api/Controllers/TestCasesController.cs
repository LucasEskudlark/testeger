using Microsoft.AspNetCore.Mvc;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.Services.TestCase;

namespace Testeger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCasesController : ControllerBase
    {
        private readonly ITestCaseService _testCaseService;

        public TestCasesController(ITestCaseService testCaseService)
        {
            _testCaseService = testCaseService;
        }

        public async Task<IActionResult> CreateTestCaseAsync([FromBody] CreateTestCaseRequest request)
        {
            var response = await _testCaseService.CreateTestCaseAsync(request);

            return Ok(response);
        }
    }
}

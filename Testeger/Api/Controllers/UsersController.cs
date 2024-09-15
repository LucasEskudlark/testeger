using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.User;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{roleName}")]
    public async Task<IActionResult> GetUsersByRoleAsync(string roleName)
    {
        var response = await _userService.GetUsersByProjectRoleAsync(roleName);

        return Ok(response);
    }
}

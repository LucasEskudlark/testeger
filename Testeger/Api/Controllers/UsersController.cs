﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("role/{roleName}")]
    public async Task<IActionResult> GetUsersByRoleAsync(string roleName)
    {
        var response = await _userService.GetUsersByProjectRoleAsync(roleName);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(string id)
    {
        var response = await _userService.GetUserByIdAsync(id);

        return Ok(response);
    }
}
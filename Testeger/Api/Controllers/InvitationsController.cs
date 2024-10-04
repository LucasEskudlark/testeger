using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.Invitation;
using Testeger.Shared.DTOs.Requests.ConfirmInvitation;
using Testeger.Shared.DTOs.Requests.SendInvitation;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvitationsController : ControllerBase
{
    private readonly IInvitationService _invitationService;

    public InvitationsController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [Authorize]
    [HttpPost("send")]
    public async Task<IActionResult> SendInvitationAsync(SendInvitationRequest request)
    {
        await _invitationService.SendInvitationAsync(request);

        return NoContent();
    }

    [Authorize]
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmInvitationAsync(ConfirmInvitationRequest request)
    {
        var response = await _invitationService.ConfirmInvitationAsync(request, User);

        return Ok(response);
    }
}
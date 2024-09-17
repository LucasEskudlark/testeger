using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.Email;
using Testeger.Application.Services.Invitation;
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

    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync(SendInvitationRequest request)
    {
        await _invitationService.SendInvitationAsync(request);

        return NoContent();
    }
}

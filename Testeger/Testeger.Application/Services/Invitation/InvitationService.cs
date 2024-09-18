using AutoMapper;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using Testeger.Application.Helpers;
using Testeger.Application.Services.Email;
using Testeger.Application.Services.Token;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Repositories.Invitations;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Requests.SendInvitation;
using DomainInvitation = Testeger.Domain.Models.Entities.Invitation;

namespace Testeger.Application.Services.Invitation;

public class InvitationService : BaseService, IInvitationService
{
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public InvitationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITokenService tokenService,
        IEmailService emailService)
        : base(unitOfWork, mapper)
    {
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task SendInvitationAsync(SendInvitationRequest request)
    {
        foreach (string email in request.Emails)
        {
            var invitationId = GenerateGuid();

            var parameters = new InvitationParameters
            {
                InvitationId = invitationId,
                Email = email,
                ProjectId = request.ProjectId
            };

            var token = GetTokenAsString(parameters);

            await GenerateAndSaveNewInvitation(parameters, token);

            var subject = EmailHelper.GetProjectInvitationEmailSubject();
            var body = EmailHelper.GetProjectInvitationEmailBody(token);

            await _emailService.SendEmailAsync(email, subject, body);
        }
    }

    private async Task GenerateAndSaveNewInvitation(InvitationParameters parameters, string token)
    {
        var invitation = new DomainInvitation
        {
            Id = parameters.InvitationId,
            Email = parameters.Email,
            InvitationToken = token,
            ProjectId = parameters.ProjectId
        };

        await _unitOfWork.InvitationRepository.AddAsync(invitation);
    }

    private string GetTokenAsString(InvitationParameters parameters)
    {
        var claims = GetInvitationTokenClaims(parameters);

        var token = _tokenService.GenerateInvitationToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static List<Claim> GetInvitationTokenClaims(InvitationParameters parameters)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Email, parameters.Email),
            new("project_id", parameters.ProjectId),
            new(JwtRegisteredClaimNames.Jti, GenerateGuid()),
            new(JwtRegisteredClaimNames.Sub, parameters.InvitationId)
        };

        return authClaims;
    }

}
public class InvitationParameters
{
    public required string InvitationId { get; set; }
    public required string Email { get; set; }
    public required string ProjectId { get; set; }
}
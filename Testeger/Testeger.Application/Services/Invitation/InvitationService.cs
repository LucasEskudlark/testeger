using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Testeger.Application.Helpers;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Email;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.Token;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.Authorization;
using Testeger.Shared.DTOs.Requests.ConfirmInvitation;
using Testeger.Shared.DTOs.Requests.SendInvitation;
using Testeger.Shared.DTOs.Responses.Invitation;
using DomainInvitation = Testeger.Domain.Models.Entities.Invitation;

namespace Testeger.Application.Services.Invitation;

public class InvitationService : BaseService, IInvitationService
{
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IProjectService _projectService;
    private readonly ICustomAuthenticationService _authService;

    private const string ProjectIdClaimType = "project_id";
    private const string ProjectRoleClaimType = "project_role";

    public InvitationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITokenService tokenService,
        IEmailService emailService,
        IProjectService projectService,
        ICustomAuthenticationService authService)
        : base(unitOfWork, mapper)
    {
        _tokenService = tokenService;
        _emailService = emailService;
        _projectService = projectService;
        _authService = authService;
    }

    public async Task SendInvitationAsync(SendInvitationRequest request)
    {
        foreach (var user in request.Users)
        {
            var invitationId = GenerateGuid();

            var parameters = new InvitationParameters
            {
                InvitationId = invitationId,
                Email = user.Email,
                ProjectId = request.ProjectId,
                Role = AuthorizationRoles.GetRoleFromRoleType(user.RoleType)
            };

            var token = GetTokenAsString(parameters);

            await GenerateAndSaveNewInvitation(parameters, token);

            var subject = EmailHelper.GetProjectInvitationEmailSubject();
            var body = EmailHelper.GetProjectInvitationEmailBody(token);

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
    }

    public async Task<ConfirmInvitationResponse> ConfirmInvitationAsync(ConfirmInvitationRequest request, ClaimsPrincipal user)
    {
        if (_tokenService.IsTokenExpired(request.Token))
        {
            return new() { IsTokenExpired = true };
        }

        var invitationParameters = GetParametersFromToken(request.Token);
        var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmail != invitationParameters.Email)
        {
            return new();
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await SaveChangesToInvitationAsync(invitationParameters.InvitationId, userId, request.Token);

        await _projectService.AddUserToProjectAsync(invitationParameters.ProjectId, userId);

        await _authService.AddUserToProjectRoleAsync(userId,
            AuthorizationRoles.GetRoleForProject(invitationParameters.ProjectId, invitationParameters.Role));

        return new() { IsSuccess = true };
    }

    private async Task SaveChangesToInvitationAsync(string invitationId, string userId, string token)
    {
        var invitation = await _unitOfWork.InvitationRepository.GetByIdAndTokenAsync(invitationId, HashToken(token));

        if (invitation is null || invitation.IsConfirmed)
        {
            throw new ArgumentNullException("Invalid or already confirmed invitation");
        }

        invitation.IsConfirmed = true;
        invitation.UserId = userId;

        await _unitOfWork.CompleteAsync();
    }

    private InvitationParameters GetParametersFromToken(string token)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(token)
            ?? throw new InvalidOperationException("Unable to extract principal from token.");

        return new InvitationParameters
        {
            InvitationId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("InvitationId claim not found in token."),
            ProjectId = principal.FindFirst(ProjectIdClaimType)?.Value
                ?? throw new InvalidOperationException("ProjectId claim not found in token."),
            Email = principal.FindFirst(ClaimTypes.Email)?.Value
                ?? throw new InvalidOperationException("Email claim not found in token."),
            Role = principal.FindFirst(ProjectRoleClaimType)?.Value
                ?? throw new InvalidOperationException("Project role claim not found in token.")
        };
    }

    private async Task GenerateAndSaveNewInvitation(InvitationParameters parameters, string token)
    {
        var invitation = new DomainInvitation
        {
            Id = parameters.InvitationId,
            Email = parameters.Email,
            InvitationToken = HashToken(token),
            ProjectId = parameters.ProjectId,
            SentDate = DateTime.Now
        };

        await _unitOfWork.InvitationRepository.AddAsync(invitation);
        await _unitOfWork.CompleteAsync();
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
            new(ProjectIdClaimType, parameters.ProjectId),
            new(JwtRegisteredClaimNames.Jti, GenerateGuid()),
            new(JwtRegisteredClaimNames.Sub, parameters.InvitationId),
            new(ProjectRoleClaimType, parameters.Role)
        };

        return authClaims;
    }

    private string HashToken(string token)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
public class InvitationParameters
{
    public required string InvitationId { get; set; }
    public required string Email { get; set; }
    public required string ProjectId { get; set; }
    public required string Role { get; set; }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Testeger.Application.Services.Authentication;
using Testeger.Application.Services.Invitation;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.Token;
using Testeger.Infra.Repositories.Invitations;
using Testeger.Shared.Authorization;
using Testeger.Shared.DTOs.Enumerations;
using Testeger.Shared.DTOs.Requests.ConfirmInvitation;
using Testeger.Shared.DTOs.Requests.SendInvitation;

namespace UnitTests.Application.Services.Invitation;

public class InvitationServiceTests : BaseServiceTests
{
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<IProjectService> _projectService;
    private readonly Mock<ICustomAuthenticationService> _authService;
    private readonly Mock<IInvitationRepository> _invitationRepository;
    private readonly ClientSettings _clientSettings;
    private readonly InvitationService _invitationService;

    private const string ProjectIdClaimType = "project_id";
    private const string ProjectRoleClaimType = "project_role";

    private const string UserEmail = "user@example.com";
    private readonly string InvitationId = Guid.NewGuid().ToString();
    private readonly string ProjectId = Guid.NewGuid().ToString();
    private readonly string UserId = Guid.NewGuid().ToString();
    private const string Role = AuthorizationRoles.Developer;

    public InvitationServiceTests()
    {
        _invitationRepository = new Mock<IInvitationRepository>();
        _unitOfWork.SetupGet(u => u.InvitationRepository).Returns(_invitationRepository.Object);

        _tokenService = new Mock<ITokenService>();
        _emailService = new Mock<IEmailService>();
        _projectService = new Mock<IProjectService>();
        _authService = new Mock<ICustomAuthenticationService>();

        _clientSettings = SetupClientSettings();
        IOptions<ClientSettings> options = Options.Create(_clientSettings);

        _invitationService = new(_unitOfWork.Object,
            _mapper.Object,
            _tokenService.Object,
            _emailService.Object,
            _projectService.Object,
            _authService.Object, options);
    }

    [Fact]
    public async Task SendInvitationAsync_GivenValidRequest_ShouldSendEmail()
    {
        var users = _fixture
            .Build<UserInvitationRequest>()
            .With(r => r.RoleType, RoleType.ProjectManager)
            .CreateMany(2);

        var request = _fixture
            .Build<SendInvitationRequest>()
            .With(r => r.Users, users)
            .Create();

        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        var token = TestUtils.MockJwtToken(claims);

        _tokenService.Setup(x => x.GenerateInvitationToken(It.IsAny<List<Claim>>()))
                .Returns(token);

        await _invitationService.SendInvitationAsync(request);

        _unitOfWork.Verify(x => x.InvitationRepository.AddAsync(It.IsAny<DomainInvitation>()), Times.Exactly(2));
        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Exactly(2));

        _emailService.Verify(x => x.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.Is<string>(s => s.Contains(_clientSettings.BaseAddress))),
            Times.Exactly(2));

        _tokenService.Verify(x => x.GenerateInvitationToken(It.IsAny<List<Claim>>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ConfirmInvitationAsync_GivenValidToken_ShouldConfirmInvitation()
    {
        _tokenService.Setup(t => t.IsTokenExpired(It.IsAny<string>()))
            .Returns(false);

        var claims = GetInvitationTokenClaims(UserEmail, ProjectId, InvitationId, Role);
        var token = TestUtils.MockJwtToken(claims);

        var request = _fixture
            .Build<ConfirmInvitationRequest>()
            .With(r => r.Token, token.ToString())
            .Create();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, UserEmail),
                new(ClaimTypes.NameIdentifier, UserId)
            ]));


        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        _tokenService.Setup(x => x.IsTokenExpired(request.Token)).Returns(false);
        _tokenService.Setup(x => x.GetPrincipalFromExpiredToken(request.Token))
            .Returns(principal);

        var invitation = new DomainInvitation
        {
            Id = InvitationId,
            Email = UserEmail,
            InvitationToken = token.ToString(),
            ProjectId = ProjectId,
            IsConfirmed = false
        };

        _unitOfWork.Setup(x => x.InvitationRepository.GetByIdAndTokenAsync(InvitationId, It.IsAny<string>()))
            .ReturnsAsync(invitation);

        
        var result = await _invitationService.ConfirmInvitationAsync(request, user);

        result.IsSuccess.Should().BeTrue();
        result.IsTokenExpired.Should().BeFalse();

        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _projectService.Verify(x => x.AddUserToProjectAsync(ProjectId, UserId), Times.Once);
        _authService.Verify(x => x.AddUserToProjectRoleAsync(
            UserId,
            It.Is<string>(s => s.Contains(AuthorizationRoles.GetRoleForProject(ProjectId, AuthorizationRoles.Developer)))),
            Times.Once);
    }

    [Fact]
    public async Task ConfirmInvitationAsync_GivenNullInvitation_ShouldThrowException()
    {
        _tokenService.Setup(t => t.IsTokenExpired(It.IsAny<string>()))
            .Returns(false);

        var claims = GetInvitationTokenClaims(UserEmail, ProjectId, InvitationId, Role);
        var token = TestUtils.MockJwtToken(claims);

        var request = _fixture
            .Build<ConfirmInvitationRequest>()
            .With(r => r.Token, token.ToString())
            .Create();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, UserEmail),
                new(ClaimTypes.NameIdentifier, UserId)
            ]));


        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        _tokenService.Setup(x => x.IsTokenExpired(request.Token)).Returns(false);
        _tokenService.Setup(x => x.GetPrincipalFromExpiredToken(request.Token))
            .Returns(principal);


        _unitOfWork.Setup(x => x.InvitationRepository.GetByIdAndTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(null as DomainInvitation);


        await _invitationService
            .Invoking(service => service.ConfirmInvitationAsync(request, user))
            .Should()
            .ThrowAsync<ArgumentNullException>();

        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _projectService.Verify(x => x.AddUserToProjectAsync(ProjectId, UserId), Times.Never);
        _authService.Verify(x => x.AddUserToProjectRoleAsync(
            UserId,
            It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ConfirmInvitationAsync_GivenConfirmedInvitation_ShouldThrowException()
    {
        _tokenService.Setup(t => t.IsTokenExpired(It.IsAny<string>()))
            .Returns(false);

        var claims = GetInvitationTokenClaims(UserEmail, ProjectId, InvitationId, Role);
        var token = TestUtils.MockJwtToken(claims);

        var request = _fixture
            .Build<ConfirmInvitationRequest>()
            .With(r => r.Token, token.ToString())
            .Create();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, UserEmail),
                new(ClaimTypes.NameIdentifier, UserId)
            ]));


        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        _tokenService.Setup(x => x.IsTokenExpired(request.Token)).Returns(false);
        _tokenService.Setup(x => x.GetPrincipalFromExpiredToken(request.Token))
            .Returns(principal);

        var invitation = new DomainInvitation
        {
            Id = InvitationId,
            Email = UserEmail,
            InvitationToken = token.ToString(),
            ProjectId = ProjectId,
            IsConfirmed = true
        };

        _unitOfWork.Setup(x => x.InvitationRepository.GetByIdAndTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(invitation);

        await _invitationService
            .Invoking(service => service.ConfirmInvitationAsync(request, user))
            .Should()
            .ThrowAsync<ArgumentNullException>();

        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _projectService.Verify(x => x.AddUserToProjectAsync(ProjectId, UserId), Times.Never);
        _authService.Verify(x => x.AddUserToProjectRoleAsync(
            UserId,
            It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ConfirmInvitationAsync_GivenDifferentEmails_ShouldNotConfirmInvitation()
    {
        _tokenService.Setup(t => t.IsTokenExpired(It.IsAny<string>()))
            .Returns(false);

        var claims = GetInvitationTokenClaims(UserEmail, ProjectId, InvitationId, Role);
        var token = TestUtils.MockJwtToken(claims);

        var request = _fixture
            .Build<ConfirmInvitationRequest>()
            .With(r => r.Token, token.ToString())
            .Create();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, "different@email.com"),
                new(ClaimTypes.NameIdentifier, UserId)
            ]));


        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        _tokenService.Setup(x => x.IsTokenExpired(request.Token)).Returns(false);
        _tokenService.Setup(x => x.GetPrincipalFromExpiredToken(request.Token))
            .Returns(principal);

        var invitation = new DomainInvitation
        {
            Id = InvitationId,
            Email = UserEmail,
            InvitationToken = token.ToString(),
            ProjectId = ProjectId,
            IsConfirmed = false
        };

        _unitOfWork.Setup(x => x.InvitationRepository.GetByIdAndTokenAsync(InvitationId, It.IsAny<string>()))
            .ReturnsAsync(invitation);

        var result = await _invitationService.ConfirmInvitationAsync(request, user);

        result.IsSuccess.Should().BeFalse();
        result.IsTokenExpired.Should().BeFalse();

        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _projectService.Verify(x => x.AddUserToProjectAsync(ProjectId, UserId), Times.Never);
        _authService.Verify(x => x.AddUserToProjectRoleAsync(
            UserId,
            It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task ConfirmInvitationAsync_GivenNullTokenPrincipal_ShouldThrowException()
    {
        _tokenService.Setup(t => t.IsTokenExpired(It.IsAny<string>()))
            .Returns(false);

        var claims = GetInvitationTokenClaims(UserEmail, ProjectId, InvitationId, Role);
        var token = TestUtils.MockJwtToken(claims);

        var request = _fixture
            .Build<ConfirmInvitationRequest>()
            .With(r => r.Token, token.ToString())
            .Create();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, UserEmail),
                new(ClaimTypes.NameIdentifier, UserId)
            ]));


        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        _tokenService.Setup(x => x.IsTokenExpired(request.Token)).Returns(false);
        _tokenService.Setup(x => x.GetPrincipalFromExpiredToken(request.Token))
            .Returns(null as ClaimsPrincipal);

        await _invitationService
            .Invoking(service => service.ConfirmInvitationAsync(request, user))
            .Should()
            .ThrowAsync<InvalidOperationException>();

        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _projectService.Verify(x => x.AddUserToProjectAsync(ProjectId, UserId), Times.Never);
        _authService.Verify(x => x.AddUserToProjectRoleAsync(
            UserId,
            It.IsAny<string>()),
            Times.Never);
    }

    private static ClientSettings SetupClientSettings()
    {
        return new() { BaseAddress = "https://fakehost:8000" };
    }

    private static List<Claim> GetInvitationTokenClaims(string email, string projectId, string invitationId, string role)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ProjectIdClaimType, projectId),
            new(ClaimTypes.NameIdentifier, invitationId),
            new(ProjectRoleClaimType, role)
        };

        return authClaims;
    }
}

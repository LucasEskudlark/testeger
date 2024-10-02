using Microsoft.AspNetCore.Identity;
using Testeger.Application.Services.Project;
using Testeger.Application.Services.Role;
using Testeger.Infra.Repositories.Projects;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses.Project;

namespace UnitTests.Application.Services.Project;

public class ProjectServiceTests : BaseServiceTests
{
    private readonly Mock<IRoleService> _roleService;
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly ProjectService _projectService;
    private const string FakeProjectId = "FakeProjectId";
    private const string FakeUserId = "FakeUserId";

    public ProjectServiceTests()
    {
        _roleService = new Mock<IRoleService>();
        _projectRepository = new Mock<IProjectRepository>();
        _unitOfWork.SetupGet(u => u.ProjectRepository).Returns(_projectRepository.Object);
        _projectService = new(_unitOfWork.Object, _mapper.Object, _roleService.Object);
    }

    [Fact]
    public async Task CreateProjectAsync_GivenValidRequest_ShouldReturnAsExpected()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .Without(p => p.ProjectUsers)
            .Create();

        var request = _fixture.Create<CreateProjectRequest>();
        _mapper.Setup(m => m.Map<DomainProject>(request)).Returns(domainProject);

        var expectedResponse = _fixture
            .Build<CreateProjectResponse>()
            .With(r => r.Id, domainProject.Id)
            .Create();

        _mapper.Setup(m => m.Map<CreateProjectResponse>(domainProject)).Returns(expectedResponse);

        var result = await _projectService.CreateProject(request);

        result.Should().BeEquivalentTo(expectedResponse);

        _unitOfWork.Verify(u => u.ProjectRepository.AddAsync(domainProject), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task GetProjectByIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .Without(p => p.ProjectUsers)
            .Create();

        var expectedResponse = _fixture.Build<GetProjectResponse>().With(x => x.Id, FakeProjectId).Create();


        _unitOfWork.Setup(uow => uow.ProjectRepository.GetProjectByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainProject);

        _mapper.Setup(m => m.Map<GetProjectResponse>(domainProject))
           .Returns(expectedResponse);

        var result = await _projectService.GetProjectById(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetAllProjectsAsync_GivenValidRequest_ShouldReturnExpectedResponse()
    {
        var pagedRequest = new PagedRequest();
        var projects = _fixture
            .CreateMany<DomainProject>(pagedRequest.PageSize);

        var expectedResult = _fixture
             .Build<PagedResult<DomainProject>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, projects)
             .Create();

        _unitOfWork.Setup(uow => uow.ProjectRepository.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var dtoTestRequests = _fixture
            .CreateMany<GetProjectResponse>(pagedRequest.PageSize);
        var expectedResponse = _fixture
             .Build<PagedResponse<GetProjectResponse>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, dtoTestRequests)
             .Create();

        _mapper.Setup(m => m.Map<PagedResponse<GetProjectResponse>>(expectedResult))
           .Returns(expectedResponse);

        var result = await _projectService.GetAllProjectsAsync(pagedRequest);

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task DeleteProjectAsync_GivenValidId_ShouldDeleteAsExpected()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .With(p => p.ProjectUsers, [])
            .Create();

        _unitOfWork.Setup(uow => uow.ProjectRepository.GetProjectByIdAsync(FakeProjectId))
            .ReturnsAsync(domainProject);

        var projectRoles = new List<IdentityRole> { new() { Name = string.Empty } };
        _roleService.Setup(r => r.GetAllProjectRolesAsync(FakeProjectId)).ReturnsAsync(projectRoles);

        await _projectService.DeleteProject(FakeProjectId);

        _roleService.Verify(rs => rs.RemoveAllUsersFromRoleAsync(It.IsAny<string>()), Times.Once);
        _roleService.Verify(rs => rs.DeleteRoleAsync(It.IsAny<string>()), Times.Once);
        _unitOfWork.Verify(uow => uow.ProjectRepository.Delete(domainProject), Times.Once);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteTestProjectAsync_GivenNonExistentId_ShouldThrowNotFoundException()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .With(p => p.ProjectUsers, [])
            .Create();

        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainProject);

        await _projectService
            .Invoking(service => service.DeleteProject(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(uow => uow.ProjectRepository.Delete(domainProject), Times.Never);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task GetProjectsForUserAsync_GivenValidId_ShouldReturnAsExpected()
    {
        var domainProjects = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .With(p => p.ProjectUsers, [])
            .CreateMany(2);

        _unitOfWork.Setup(u => u.ProjectRepository.GetProjectsForUserAsync(It.IsAny<string>())).ReturnsAsync(domainProjects);

        var expectedResponse = _fixture
            .Build<GetProjectResponse>()
            .With(p => p.Id, FakeProjectId)
            .CreateMany(2);
        _mapper.Setup(m => m.Map<IEnumerable<GetProjectResponse>>(domainProjects))
           .Returns(expectedResponse);

        var result = await _projectService.GetProjectsForUserAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task AddUserToProjectAsync_GivenValidProjectIdAndUserId_ShouldAddUserAsExpected()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .With(p => p.ProjectUsers, [])
            .Create();

        _unitOfWork.Setup(uow => uow.ProjectRepository.GetProjectByIdAsync(FakeProjectId))
            .ReturnsAsync(domainProject);

        await _projectService.AddUserToProjectAsync(FakeProjectId, FakeUserId);

        domainProject.ProjectUsers.Count.Should().Be(1);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }


    [Fact]
    public async Task AddUserToProjectAsync_GivenNonExistentProjectIdAndUserId_ShouldAddUserAsExpected()
    {
        var domainProject = _fixture
            .Build<DomainProject>()
            .With(p => p.Id, FakeProjectId)
            .With(p => p.ProjectUsers, [])
            .Create();

        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainProject);

        await _projectService
            .Invoking(service => service.AddUserToProjectAsync(FakeProjectId, FakeUserId))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }
}

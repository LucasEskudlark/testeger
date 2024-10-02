using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses.TestRequest;
using DomainTestRequest = Testeger.Domain.Models.Entities.TestRequest;
namespace UnitTests.Application.Services.TestRequest;

public class TestRequestServiceTests : BaseServiceTests
{
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<IUserService> _userService;
    private readonly TestRequestService _testRequestService;
    private readonly ClientSettings _clientSettings;

    private const string FakeRequestId = "FakeRequestId";
    private const string FakeProjectId = "FakeProjectId";

    public TestRequestServiceTests()
    {
        _emailService = new Mock<IEmailService>();
        _userService = new Mock<IUserService>();
        _clientSettings = new() { BaseAddress = string.Empty };
        IOptions<ClientSettings> options = Options.Create(_clientSettings);

        _testRequestService = new TestRequestService(_unitOfWork.Object, _mapper.Object, _emailService.Object, _userService.Object, options);
    }

    [Fact]
    public async Task GetTestRequestByIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var domainTestRequest = new DomainTestRequest { Id = FakeRequestId, CreatedByUserId = string.Empty };
        var expectedResponse = _fixture.Build<GetTestRequestResponse>().With(x => x.Id, FakeRequestId).Create();


        _unitOfWork.Setup(uow => uow.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainTestRequest);

        _mapper.Setup(m => m.Map<GetTestRequestResponse>(domainTestRequest))
           .Returns(expectedResponse);

        var result = await _testRequestService.GetTestRequestByIdAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetTestRequestByIdAsync_GivenTestRequestDoesNotExist_ShouldThrowNotFoundException()
    {
        _unitOfWork
            .Setup(u => u.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(null as DomainTestRequest);

        await _testRequestService
            .Invoking(service => service.GetTestRequestByIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(u => u.TestRequestRepository.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mapper.Verify(m => m.Map<GetTestRequestResponse>(It.IsAny<DomainTestRequest>()), Times.Never);
    }

    [Fact]
    public async Task CreateTestRequestAsync_GivenValidRequest_ShouldReturnExpectedResponse()
    {
        var project = _fixture
            .Build<Project>()
            .With(x => x.Id, FakeProjectId)
            .Create();

        _unitOfWork
            .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(project);

        var expectedEntityResponse = _fixture
            .Build<DomainTestRequest>()
            .With(x => x.ProjectId, FakeProjectId)
            .Create();
        _mapper.Setup(m => m.Map<DomainTestRequest>(It.IsAny<CreateTestRequestRequest>()))
            .Returns(expectedEntityResponse);

        const int fakeRequestNumber = 1;
        _unitOfWork
            .Setup(u => u.TestRequestRepository.GetNextNumberAsync(It.IsAny<string>()))
            .ReturnsAsync(fakeRequestNumber);

        var expectedDtoResponse = _fixture
            .Build<CreateTestRequestResponse>()
            .With(x => x.Id, FakeRequestId)
            .Create();
        _mapper.Setup(m => m.Map<CreateTestRequestResponse>(It.IsAny<DomainTestRequest>()))
            .Returns(expectedDtoResponse);

        var request = _fixture.Build<CreateTestRequestRequest>()
            .With(x => x.ProjectId, FakeProjectId)
            .Create();

        var result = await _testRequestService.CreateTestRequestAsync(request);

        result.Should().BeEquivalentTo(expectedDtoResponse);

        _unitOfWork.Verify(u => u.TestRequestRepository.AddAsync(expectedEntityResponse), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateTestRequestAsync_GivenInvalidProject_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as Project);

        var request = _fixture.Build<CreateTestRequestRequest>()
            .With(x => x.ProjectId, FakeProjectId)
            .Create();
        await _testRequestService
            .Invoking(service => service.CreateTestRequestAsync(request))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _unitOfWork.Verify(u => u.TestRequestRepository.AddAsync(It.IsAny<DomainTestRequest>()), Times.Never);
        _mapper.Verify(m => m.Map<DomainTestRequest>(It.IsAny<CreateTestRequestRequest>()), Times.Never);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task GetAllTestRequestsAsync_GivenValidRequest_ShouldReturnExpectedResponse()
    {
        var pagedRequest = new PagedRequest();
        var testRequests = _fixture
            .CreateMany<DomainTestRequest>(pagedRequest.PageSize);

        var expectedResult = _fixture
             .Build<PagedResult<DomainTestRequest>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, testRequests)
             .Create();

        _unitOfWork.Setup(uow => uow.TestRequestRepository.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var dtoTestRequests = _fixture
            .CreateMany<GetTestRequestResponse>(pagedRequest.PageSize);
        var expectedResponse = _fixture
             .Build<PagedResponse<GetTestRequestResponse>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, dtoTestRequests)
             .Create();

        _mapper.Setup(m => m.Map<PagedResponse<GetTestRequestResponse>>(expectedResult))
           .Returns(expectedResponse);

        var result = await _testRequestService.GetAllTestRequestsPagedAsync(pagedRequest);

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task DeleteTestRequestAsync_GivenValidId_ShouldDeleteAsExpected()
    {
        var domainTestRequest = new DomainTestRequest { Id = FakeRequestId, CreatedByUserId = string.Empty };

        _unitOfWork.Setup(uow => uow.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainTestRequest);

        await _testRequestService.DeleteTestRequestAsync(FakeRequestId);

        _unitOfWork.Verify(uow => uow.TestRequestRepository.Delete(domainTestRequest), Times.Once);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteTestRequestAsync_GivenNonExistentId_ShouldThrowNotFoundException()
    {
        var domainTestRequest = new DomainTestRequest { Id = FakeRequestId, CreatedByUserId = string.Empty };

        _unitOfWork
           .Setup(u => u.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainTestRequest);

        await _testRequestService
            .Invoking(service => service.DeleteTestRequestAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(uow => uow.TestRequestRepository.Delete(domainTestRequest), Times.Never);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task gETTestRequestsByProjectIdAsync_GivenInvalidProject_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as Project);

        await _testRequestService
            .Invoking(service => service.GetTestRequestsByProjectIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();


        _mapper.Verify(m => m.Map<DomainTestRequest>(It.IsAny<IEnumerable<GetTestRequestResponse>>()), Times.Never);
    }
}

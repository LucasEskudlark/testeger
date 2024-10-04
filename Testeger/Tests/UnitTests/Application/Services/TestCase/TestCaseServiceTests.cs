using Testeger.Application.Services.TestCase;
using Testeger.Infra.Repositories.TestCases;
using Testeger.Infra.Repositories.TestRequests;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses.TestCase;
using Testeger.Shared.DTOs.Responses.TestRequest;

namespace UnitTests.Application.Services.TestCase;

public class TestCaseServiceTests : BaseServiceTests
{
    private readonly TestCaseService _testCaseService;
    private readonly Mock<ITestCaseRepository> _testCaseRepositoryMock;
    private const string FakeTestRequestId = "FakeTestRequestId";
    private const string FakeTestCaseId = "FakeTestCaseId";
    private const string FakeProjectId = "FakeProjectId";

    public TestCaseServiceTests()
    {
        _testCaseRepositoryMock = new Mock<ITestCaseRepository>();
        _unitOfWork.SetupGet(uow => uow.TestCaseRepository).Returns(_testCaseRepositoryMock.Object);
        _testCaseService = new(_unitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateTestCaseAsync_GivenValidRequest_ShouldCreateAsExpected()
    {
        var request = _fixture
            .Build<CreateTestCaseRequest>()
            .With(x => x.TestRequestId, FakeTestRequestId)
            .With(x => x.ProjectId, FakeProjectId)
            .Create();

        var testRequest = _fixture.Create<DomainTestRequest>();
        _unitOfWork
            .Setup(u => u.TestRequestRepository.GetByIdAsync(FakeTestRequestId))
            .ReturnsAsync(testRequest);

        var project = _fixture.Create<DomainProject>();
        _unitOfWork
            .Setup(u => u.ProjectRepository.GetByIdAsync(FakeProjectId))
            .ReturnsAsync(project);

        var testCase = _fixture.Create<DomainTestCase>();
        _mapper.Setup(m => m.Map<DomainTestCase>(request))
            .Returns(testCase);

        var expectedResponse = new CreateTestCaseResponse { Id = FakeTestCaseId };
        _mapper.Setup(m => m.Map<CreateTestCaseResponse>(testCase))
            .Returns(expectedResponse);

        var result = await _testCaseService.CreateTestCaseAsync(request);

        result.Should().BeEquivalentTo(expectedResponse);

        _unitOfWork.Verify(u => u.TestCaseRepository.AddAsync(testCase), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateTestCaseAsync_GivenInvalidRequestId_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainTestRequest);

        var request = _fixture
            .Create<CreateTestCaseRequest>();

        await _testCaseService
            .Invoking(service => service.CreateTestCaseAsync(request))
            .Should()
            .ThrowAsync<NotFoundException>();

        _mapper.Verify(m => m.Map<DomainTestCase>(It.IsAny<CreateTestCaseRequest>()), Times.Never);
        _mapper.Verify(m => m.Map<CreateTestCaseResponse>(It.IsAny<DomainTestCase>()), Times.Never);

        _unitOfWork.Verify(u => u.TestCaseRepository.AddAsync(It.IsAny<DomainTestCase>()), Times.Never);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task CreateTestCaseAsync_GivenInvalidProjectId_ShouldThrowNotFoundException()
    {
        var testRequest = _fixture.Create<DomainTestRequest>();
        _unitOfWork
            .Setup(u => u.TestRequestRepository.GetByIdAsync(FakeTestRequestId))
            .ReturnsAsync(testRequest);

        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainProject);

        var request = _fixture
            .Create<CreateTestCaseRequest>();

        await _testCaseService
            .Invoking(service => service.CreateTestCaseAsync(request))
            .Should()
            .ThrowAsync<NotFoundException>();

        _mapper.Verify(m => m.Map<DomainTestCase>(It.IsAny<CreateTestCaseRequest>()), Times.Never);
        _mapper.Verify(m => m.Map<CreateTestCaseResponse>(It.IsAny<DomainTestCase>()), Times.Never);

        _unitOfWork.Verify(u => u.TestCaseRepository.AddAsync(It.IsAny<DomainTestCase>()), Times.Never);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task GetTestCaseByIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var domainTestCase = new DomainTestCase { Id = FakeTestCaseId, CreatedByUserId = string.Empty };
        var expectedResponse = _fixture.Build<GetTestCaseResponse>().With(x => x.Id, FakeTestCaseId).Create();


        _unitOfWork.Setup(uow => uow.TestCaseRepository.GetTestCaseByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainTestCase);

        _mapper.Setup(m => m.Map<GetTestCaseResponse>(domainTestCase))
           .Returns(expectedResponse);

        var result = await _testCaseService.GetTestCaseByIdAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetTestCaseByIdAsync_GivenTestCaseDoesNotExist_ShouldThrowNotFoundException()
    {
        _unitOfWork.Setup(uow => uow.TestCaseRepository.GetTestCaseByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(null as DomainTestCase);

        await _testCaseService
            .Invoking(service => service.GetTestCaseByIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(u => u.TestCaseRepository.GetTestCaseByIdAsync(It.IsAny<string>()), Times.Once);
        _mapper.Verify(m => m.Map<GetTestCaseResponse>(It.IsAny<DomainTestCase>()), Times.Never);
    }

    [Fact]
    public async Task GetAllTestCasesAsync_GivenValidRequest_ShouldReturnExpectedResponse()
    {
        var pagedRequest = new PagedRequest();
        var testRequests = _fixture
            .CreateMany<DomainTestCase>(pagedRequest.PageSize);

        var expectedResult = _fixture
             .Build<PagedResult<DomainTestCase>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, testRequests)
             .Create();

        _unitOfWork.Setup(uow => uow.TestCaseRepository.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var dtoTestRequests = _fixture
            .CreateMany<GetTestCaseResponse>(pagedRequest.PageSize);
        var expectedResponse = _fixture
             .Build<PagedResponse<GetTestCaseResponse>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, dtoTestRequests)
             .Create();

        _mapper.Setup(m => m.Map<PagedResponse<GetTestCaseResponse>>(expectedResult))
           .Returns(expectedResponse);

        var result = await _testCaseService.GetAllTestCasesPagedAsync(pagedRequest);

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetTestCasesByTestRequestIdAsync_GivenInvalidTestRequest_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.TestRequestRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainTestRequest);

        await _testCaseService
            .Invoking(service => service.GetTestCasesByTestRequestIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();


        _mapper.Verify(m => m.Map<DomainTestCase>(It.IsAny<IEnumerable<GetTestCaseResponse>>()), Times.Never);
    }

    [Fact]
    public async Task GetTestCasesByProjectIdAsync_GivenInvalidProject_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.ProjectRepository.GetByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainProject);

        await _testCaseService
            .Invoking(service => service.GetTestCasesByProjectIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();


        _mapper.Verify(m => m.Map<DomainTestCase>(It.IsAny<IEnumerable<GetTestCaseResponse>>()), Times.Never);
    }

    [Fact]
    public async Task DeleteTestCaseAsync_GivenValidId_ShouldDeleteAsExpected()
    {
        var domainTestCase = new DomainTestCase { Id = FakeTestCaseId, CreatedByUserId = string.Empty };

        _unitOfWork.Setup(uow => uow.TestCaseRepository.GetTestCaseByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainTestCase);

        await _testCaseService.DeleteTestCaseAsync(FakeTestCaseId);

        _unitOfWork.Verify(uow => uow.TestCaseRepository.Delete(domainTestCase), Times.Once);
        _unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }
}

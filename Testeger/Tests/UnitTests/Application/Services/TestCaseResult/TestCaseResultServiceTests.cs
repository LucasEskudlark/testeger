using Testeger.Application.Services.TestCaseResult;
using Testeger.Infra.Repositories.TestCaseResults;
using Testeger.Infra.Repositories.TestCases;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.FinishTestCaseResult;
using Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;
using Testeger.Shared.DTOs.Responses.TestCase;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace UnitTests.Application.Services.TestCaseResult;

public class TestCaseResultServiceTests : BaseServiceTests
{
    private TestCaseResultService _testCaseResultService;
    private Mock<ITestCaseRepository> _testCaseRepository;
    private Mock<ITestCaseResultRepository> _testCaseResultRepository;
    private const string FakeTestCaseResultId = "FakeTestCaseResultId";
    private const string FakeTestCaseId = "FakeTestCaseId";

    public TestCaseResultServiceTests()
    {
        _testCaseRepository = new Mock<ITestCaseRepository>();
        _testCaseResultRepository = new Mock<ITestCaseResultRepository>();

        _unitOfWork.SetupGet(u => u.TestCaseRepository).Returns(_testCaseRepository.Object);
        _unitOfWork.SetupGet(u => u.TestCaseResultRepository).Returns(_testCaseResultRepository.Object);
        _testCaseResultService = new(_unitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateTestCaseResultAsync_GivenValidRequest_ShouldCreateAsExpected()
    {
        var request = _fixture
            .Build<CreateTestCaseResultRequest>()
            .With(x => x.TestCaseId, FakeTestCaseId)
            .Create();

        var testCase = _fixture.Create<DomainTestCase>();
        _unitOfWork
            .Setup(u => u.TestCaseRepository.GetTestCaseByIdAsync(FakeTestCaseId))
            .ReturnsAsync(testCase);

        var testCaseResult = _fixture
            .Build<DomainTestCaseResult>()
            .With(x => x.Id, FakeTestCaseResultId)
            .Create();
        _mapper.Setup(m => m.Map<DomainTestCaseResult>(request))
            .Returns(testCaseResult);

        const int resultNumber = 1;
        _unitOfWork.Setup(u => u.TestCaseResultRepository.GetNextNumberAsync(FakeTestCaseId))
            .ReturnsAsync(resultNumber);


        var expectedResponse = new CreateTestCaseResultResponse { Id = FakeTestCaseResultId };
        _mapper.Setup(m => m.Map<CreateTestCaseResultResponse>(testCaseResult))
            .Returns(expectedResponse);

        var result = await _testCaseResultService.CreateTestCaseResultAsync(request);

        result.Should().BeEquivalentTo(expectedResponse);

        _unitOfWork.Verify(u => u.TestCaseResultRepository.AddAsync(testCaseResult), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateTestCaseResultAsync_GivenInvalidTestCaseId_ShouldThrowNotFoundException()
    {
        _unitOfWork
           .Setup(u => u.TestCaseRepository.GetTestCaseByIdAsync(It.IsAny<string>()))
           .ReturnsAsync(null as DomainTestCase);

        var request = _fixture
            .Create<CreateTestCaseResultRequest>();

        await _testCaseResultService
            .Invoking(service => service.CreateTestCaseResultAsync(request))
            .Should()
            .ThrowAsync<NotFoundException>();

        _mapper.Verify(m => m.Map<DomainTestCaseResult>(It.IsAny<CreateTestCaseResultRequest>()), Times.Never);
        _mapper.Verify(m => m.Map<CreateTestCaseResultResponse>(It.IsAny<DomainTestCaseResult>()), Times.Never);

        _unitOfWork.Verify(u => u.TestCaseResultRepository.GetNextNumberAsync(It.IsAny<string>()), Times.Never);
        _unitOfWork.Verify(u => u.TestCaseResultRepository.AddAsync(It.IsAny<DomainTestCaseResult>()), Times.Never);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task GetAllTestCaseResultsAsync_GivenValidRequest_ShouldReturnExpectedResponse()
    {
        var pagedRequest = new PagedRequest();
        var testCaseResults = _fixture
            .CreateMany<DomainTestCaseResult>(pagedRequest.PageSize);

        var expectedResult = _fixture
             .Build<PagedResult<DomainTestCaseResult>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, testCaseResults)
             .Create();

        _unitOfWork.Setup(uow => uow.TestCaseResultRepository.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var dtoTestCaseResults = _fixture
            .CreateMany<GetTestCaseResultResponse>(pagedRequest.PageSize);
        var expectedResponse = _fixture
             .Build<PagedResponse<GetTestCaseResultResponse>>()
             .With(x => x.PageSize, pagedRequest.PageSize)
             .With(x => x.PageNumber, pagedRequest.PageNumber)
             .With(x => x.Items, dtoTestCaseResults)
             .Create();

        _mapper.Setup(m => m.Map<PagedResponse<GetTestCaseResultResponse>>(expectedResult))
           .Returns(expectedResponse);

        var result = await _testCaseResultService.GetAllTestCaseResultsPagedAsync(pagedRequest);

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetTestCaseResultByIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var domainTestCaseResult = new DomainTestCaseResult { Id = FakeTestCaseResultId, TestCaseId = FakeTestCaseId };

        var expectedResponse = _fixture.Build<GetTestCaseResultResponse>().With(x => x.Id, FakeTestCaseResultId).Create();


        _unitOfWork.Setup(uow => uow.TestCaseResultRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(domainTestCaseResult);

        _mapper.Setup(m => m.Map<GetTestCaseResultResponse>(domainTestCaseResult))
           .Returns(expectedResponse);

        var result = await _testCaseResultService.GetTestCaseResultByIdAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetTestCaseResultByIdAsync_GivenTestCaseDoesNotExist_ShouldThrowNotFoundException()
    {
        _unitOfWork.Setup(uow => uow.TestCaseResultRepository.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(null as DomainTestCaseResult);

        await _testCaseResultService
            .Invoking(service => service.GetTestCaseResultByIdAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWork.Verify(u => u.TestCaseResultRepository.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mapper.Verify(m => m.Map<GetTestCaseResultResponse>(It.IsAny<DomainTestCaseResult>()), Times.Never);
    }

    [Fact]
    public async Task GetTestCaseResultsByTestCaseIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var results = _fixture
            .Build<DomainTestCaseResult>()
            .With(x => x.Id, FakeTestCaseResultId)
            .CreateMany(2);

        _unitOfWork.Setup(uow => uow.TestCaseResultRepository.GetResultsByTestCaseId(It.IsAny<string>()))
            .ReturnsAsync(results);

        var expectedResponse = _fixture
            .Build<GetTestCaseResultResponse>()
            .With(x => x.Id, FakeTestCaseResultId)
            .CreateMany(2);
        _mapper.Setup(m => m.Map<IEnumerable<GetTestCaseResultResponse>>(results))
           .Returns(expectedResponse);

        var result = await _testCaseResultService.GetResultsByTestCaseIdAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task FinishTestCaseResultAsync_GivenValidRequestWithNullId_ShouldCreateResultAsExpected()
    {
        var request = _fixture
            .Build<FinishTestCaseResultRequest>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .Without(r => r.Id)
            .Create();

        var domainResult = _fixture
            .Build<DomainTestCaseResult>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .Without(r => r.Id)
            .Create();
        _mapper.Setup(m => m.Map<DomainTestCaseResult>(request)).Returns(domainResult);

        var creationRequest = _fixture
            .Build<CreateTestCaseResultRequest>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .Create();
        _mapper.Setup(m => m.Map<CreateTestCaseResultRequest>(domainResult)).Returns(creationRequest);

        var testCase = _fixture.Create<DomainTestCase>();
        _unitOfWork
            .Setup(u => u.TestCaseRepository.GetTestCaseByIdAsync(FakeTestCaseId))
            .ReturnsAsync(testCase);

        _mapper.Setup(m => m.Map<DomainTestCaseResult>(creationRequest))
            .Returns(domainResult);

        const int resultNumber = 1;
        _unitOfWork.Setup(u => u.TestCaseResultRepository.GetNextNumberAsync(FakeTestCaseId))
            .ReturnsAsync(resultNumber);

        var expectedResponse = new CreateTestCaseResultResponse { Id = FakeTestCaseResultId };
        _mapper.Setup(m => m.Map<CreateTestCaseResultResponse>(domainResult))
            .Returns(expectedResponse);

        var result = await _testCaseResultService.FinishTestCaseResultAsync(request);

        result.Should().BeEquivalentTo(expectedResponse);

        _unitOfWork.Verify(u => u.TestCaseResultRepository.AddAsync(domainResult), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task FinishTestCaseResultAsync_GivenValidRequestWithExistingId_ShouldUpdateResultAsExpected()
    {
        var request = _fixture
            .Build<FinishTestCaseResultRequest>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .With(r => r.Id, FakeTestCaseResultId)
            .Create();

        var domainResult = _fixture
            .Build<DomainTestCaseResult>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .With(r => r.Id, FakeTestCaseResultId)
            .Create();
        _mapper.Setup(m => m.Map<DomainTestCaseResult>(request)).Returns(domainResult);

        var updateRequest = _fixture
            .Build<UpdateTestCaseResultRequest>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .With(r => r.Id, FakeTestCaseResultId)
            .Create();
        _mapper.Setup(m => m.Map<UpdateTestCaseResultRequest>(domainResult)).Returns(updateRequest);

        _unitOfWork
            .Setup(u => u.TestCaseResultRepository.GetByIdAsync(FakeTestCaseResultId))
            .ReturnsAsync(domainResult);
        _mapper.Setup(m => m.Map(updateRequest, domainResult)).Returns(domainResult);

        var result = await _testCaseResultService.FinishTestCaseResultAsync(request);

        var expectedResponse = new CreateTestCaseResultResponse { Id = updateRequest.Id };
        result.Should().BeEquivalentTo(expectedResponse);

        _unitOfWork.Verify(u => u.TestCaseResultRepository.UpdateTestCaseResult(domainResult), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateTestCaseResultAsync_GivenValidRequest_ShouldUpdateAsExpected()
    {
        var domainResult = _fixture
            .Build<DomainTestCaseResult>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .With(r => r.Id, FakeTestCaseResultId)
            .Create();

        var updateRequest = _fixture
            .Build<UpdateTestCaseResultRequest>()
            .With(r => r.TestCaseId, FakeTestCaseId)
            .With(r => r.Id, FakeTestCaseResultId)
            .Create();

        _unitOfWork
            .Setup(u => u.TestCaseResultRepository.GetByIdAsync(FakeTestCaseResultId))
            .ReturnsAsync(domainResult);
        
        _mapper.Setup(m => m.Map(updateRequest, domainResult)).Returns(domainResult);

        await _testCaseResultService.UpdateTestCaseResultAsync(updateRequest);

        _unitOfWork.Verify(u => u.TestCaseResultRepository.UpdateTestCaseResult(domainResult), Times.Once);
        _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }
}

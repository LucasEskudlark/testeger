using AutoFixture;
using FluentAssertions;
using Moq;
using Testeger.Shared.DTOs.Responses.TestRequest;
using DomainTestRequest = Testeger.Domain.Models.Entities.TestRequest;
namespace UnitTests.Application.Services.TestRequest;

public class TestRequestServiceTests : TestRequestServiceTestBase
{
    private const string FakeRequestId = "FakeId";

    [Fact]
    public async Task GetTestRequestByIdAsync_GivenExistingId_ShouldReturnExpectedResponse()
    {
        var domainTestRequest = new DomainTestRequest { Id = FakeRequestId, CreatedByUserId = string.Empty };
        var expectedResponse = _fixture.Build<GetTestRequestResponse>().With(x => x.Id, FakeRequestId).Create();

        
        _unitOfWork.Setup(uow => uow.TestRequestRepository.GetByIdAsync(FakeRequestId))
            .ReturnsAsync(domainTestRequest);

         _mapper.Setup(m => m.Map<GetTestRequestResponse>(domainTestRequest))
            .Returns(expectedResponse);

        var result = await _testRequestService.GetTestRequestByIdAsync(FakeRequestId);

        result.Should().BeEquivalentTo(expectedResponse);
    }
}

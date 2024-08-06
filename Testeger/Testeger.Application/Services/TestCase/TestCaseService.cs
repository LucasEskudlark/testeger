using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.DTOs.Responses.TestCase;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
using DomainTestCase = Testeger.Domain.Models.Entities.TestCase;

namespace Testeger.Application.Services.TestCase;

public class TestCaseService : BaseService, ITestCaseService
{
    public TestCaseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateTestCaseResponse> CreateTestCaseAsync(CreateTestCaseRequest request)
    {
        await ValidateTestRequestExistenceAsync(request.TestRequestId);
        await ValidateProjectExistenceAsync(request.ProjectId);


        var testCase = _mapper.Map<DomainTestCase>(request);

        testCase.Id = GenerateGuid();
        testCase.CreatedDate = DateTime.Now;
        testCase.NeedCorrection = false;

        await _unitOfWork.TestCaseRepository.AddAsync(testCase);
        await _unitOfWork.CompleteAsync();

        var response = _mapper.Map<CreateTestCaseResponse>(testCase);

        return response;
    }

    private async Task ValidateTestRequestExistenceAsync(string testRequestId)
    {
        _ = await _unitOfWork.TestRequestRepository.GetByIdAsync(testRequestId) ??
            throw new NotFoundException($"You must inform an existing test request. TestRequest with id {testRequestId} not found");
    }

    private async Task ValidateProjectExistenceAsync(string projectId)
    {
        _ = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId)
            ?? throw new NotFoundException($"You must inform an existing project. Project with id {projectId} not found");
    }

}

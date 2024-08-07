using AutoMapper;
using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.DTOs.Responses;
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

    public async Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id)
    {
        var testCase = await _unitOfWork.TestCaseRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"TestCase with id {id} not found");

        var response = _mapper.Map<GetTestCaseResponse>(testCase);

        return response;
    }

    public async Task<PagedResponse<GetTestCaseResponse>> GetAllTestCasesPagedAsync(PagedRequest request)
    {
        var testCases = await _unitOfWork.TestCaseRepository.GetAllPagedAsync(request.PageSize, request.PageNumber);

        var response = _mapper.Map<PagedResponse<GetTestCaseResponse>>(testCases);

        return response;
    }

    public async Task DeleteTestCaseAsync(string id)
    {
        var testCase = await _unitOfWork.TestCaseRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"TestCase with id {id} not found");

        await _unitOfWork.TestCaseRepository.Delete(testCase);
        await _unitOfWork.CompleteAsync();
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

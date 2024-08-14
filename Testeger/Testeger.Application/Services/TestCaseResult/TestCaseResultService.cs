using AutoMapper;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.FinishTestCaseResult;
using Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestCaseResult;
using DomainTestCaseResult = Testeger.Domain.Models.Entities.TestCaseResult;

namespace Testeger.Application.Services.TestCaseResult;

public class TestCaseResultService : BaseService, ITestCaseResultService
{
    public TestCaseResultService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateTestCaseResultResponse> CreateTestCaseResultAsync(CreateTestCaseResultRequest request)
    {
        await ValidateTestCaseExistence(request.TestCaseId);

        var testCaseResult = _mapper.Map<DomainTestCaseResult>(request);

        testCaseResult.Id = GenerateGuid();
        testCaseResult.Number = await GetTestCaseResultNumberAsync(request.TestCaseId);

        var response = _mapper.Map<CreateTestCaseResultResponse>(testCaseResult);

        await _unitOfWork.TestCaseResultRepository.AddAsync(testCaseResult);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    public async Task<PagedResponse<GetTestCaseResultResponse>> GetAllTestCaseResultsPagedAsync(PagedRequest request)
    {
        var testCaseResults = await _unitOfWork.TestCaseResultRepository.GetAllPagedAsync(request.PageSize, request.PageNumber);

        var response = _mapper.Map<PagedResponse<GetTestCaseResultResponse>>(testCaseResults);

        return response;
    }

    public async Task<GetTestCaseResultResponse> GetTestCaseResultByIdAsync(string id)
    {
        var testCaseResult = await FindTestCaseResultByIdAsync(id);

        var response = _mapper.Map<GetTestCaseResultResponse>(testCaseResult);

        return response;
    }

    public async Task<IEnumerable<GetTestCaseResultResponse>> GetResultsByTestCaseIdAsync(string testCaseId)
    {
        var testCaseResults = await _unitOfWork.TestCaseResultRepository.GetResultsByTestCaseId(testCaseId);

        var response = _mapper.Map<IEnumerable<GetTestCaseResultResponse>>(testCaseResults);

        return response;
    }

    public async Task UpdateTestCaseResultAsync(UpdateTestCaseResultRequest request)
    {
        var testCaseResult = await FindTestCaseResultByIdAsync(request.Id);

        _mapper.Map(request, testCaseResult);

        await _unitOfWork.TestCaseResultRepository.UpdateTestCaseResult(testCaseResult);
        await _unitOfWork.CompleteAsync();
    }

    public async Task FinishTestCaseResultAsync(FinishTestCaseResultRequest request)
    {
        var testCaseResult = _mapper.Map<DomainTestCaseResult>(request);

        if (testCaseResult.Id is null)
        {
            var creationRequest = _mapper.Map<CreateTestCaseResultRequest>(testCaseResult);
            await CreateTestCaseResultAsync(creationRequest);
            return;
        }

        var updateRequest = _mapper.Map<UpdateTestCaseResultRequest>(testCaseResult);
        await UpdateTestCaseResultAsync(updateRequest);
    }

    private async Task ValidateTestCaseExistence(string testCaseId)
    {
        _ = await FindTestCaseResultByIdAsync(testCaseId);
    }

    private async Task<int> GetTestCaseResultNumberAsync(string testCaseId)
    {
        return await _unitOfWork.TestCaseResultRepository.GetNextNumberAsync(testCaseId);
    }

    private async Task<DomainTestCaseResult> FindTestCaseResultByIdAsync(string id)
    {
        return await _unitOfWork.TestCaseResultRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"TestCaseResult with id {id} not found");
    }
}

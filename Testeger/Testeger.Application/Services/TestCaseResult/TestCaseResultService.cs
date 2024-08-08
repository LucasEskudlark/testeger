using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.DTOs.Responses.TestCaseResult;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
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
        testCaseResult.IsSuccess = false;

        var response = _mapper.Map<CreateTestCaseResultResponse>(testCaseResult);

        await _unitOfWork.TestCaseResultRepository.AddAsync(testCaseResult);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    private async Task ValidateTestCaseExistence(string testCaseId)
    {
        _ = await _unitOfWork.TestCaseRepository.GetByIdAsync(testCaseId)
            ?? throw new NotFoundException($"You must inform an existing test case. TestCase with id {testCaseId} not found");
    }

    private async Task<int> GetTestCaseResultNumberAsync(string testCaseId)
    {
        return await _unitOfWork.TestCaseResultRepository.GetNextNumberAsync(testCaseId);
    }
}

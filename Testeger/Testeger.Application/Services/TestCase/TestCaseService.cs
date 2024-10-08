﻿using AutoMapper;
using Testeger.Application.Exceptions;
using Testeger.Domain.Enumerations;
using Testeger.Domain.Models.ValueObjects;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.UpdateTestCase;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestCase;
using DomainTestCase = Testeger.Domain.Models.Entities.TestCase;

namespace Testeger.Application.Services.TestCase;

public class TestCaseService : BaseUpdateService<DomainTestCase, UpdateTestCaseRequest>, ITestCaseService
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
        testCase.Status = TestCaseStatus.Pending;

        var history = GetTestCaseHistory(request.UserId, TestCaseStatus.None, TestCaseStatus.Pending);

        testCase.History.Add(history);

        var response = _mapper.Map<CreateTestCaseResponse>(testCase);

        await _unitOfWork.TestCaseRepository.AddAsync(testCase);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    public async Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id)
    {
        var testCase = await FindEntityByIdAsync(id);

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
        var testCase = await FindEntityByIdAsync(id);

        await _unitOfWork.TestCaseRepository.Delete(testCase);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByTestRequestIdAsync(string testRequestId)
    {
        await ValidateTestRequestExistenceAsync(testRequestId);

        var testCases = await _unitOfWork.TestCaseRepository.GetTestCasesByTestRequestIdAsync(testRequestId);

        var response = _mapper.Map<IEnumerable<GetTestCaseResponse>>(testCases);

        return response;
    }

    public async Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByProjectIdAsync(string projectId)
    {
        await ValidateProjectExistenceAsync(projectId);

        var testCases = await _unitOfWork.TestCaseRepository.GetTestCasesByProjectIdAsync(projectId);

        var response = _mapper.Map<IEnumerable<GetTestCaseResponse>>(testCases);
        return response;
    }

    public async Task UpdateTestCaseAsync(UpdateTestCaseRequest request)
    {
        var existingEntity = await FindEntityByIdAsync(request.Id);

        var updatedProperties = UpdateEntityPropertiesAsync(existingEntity, request);

        if (updatedProperties.Count > 0)
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    protected override void OnPropertyUpdated(DomainTestCase entity, string propertyName, object oldValue, object newValue)
    {
        if (propertyName == "Status")
        {
            var history = GetTestCaseHistory(entity.CreatedByUserId, (TestCaseStatus)oldValue, (TestCaseStatus)newValue);
            entity.History.Add(history);

            if ((TestCaseStatus)newValue is TestCaseStatus.Completed)
            {
                entity.CompletedDate = DateTime.Now;
            }
        }
    }

    protected override async Task<DomainTestCase> FindEntityByIdAsync(string id)
    {
        var testCase = await _unitOfWork.TestCaseRepository.GetTestCaseByIdAsync(id) ??
            throw new NotFoundException($"TestCase with id {id} not found");

        return testCase;
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

    private static TestCaseHistory GetTestCaseHistory(
        string userId,
        TestCaseStatus oldStatus,
        TestCaseStatus newStatus)
    {
        return new TestCaseHistory
        {
            ChangedByUserId = userId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedDate = DateTime.Now
        };
    }
}

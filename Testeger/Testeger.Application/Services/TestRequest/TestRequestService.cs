﻿using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.DTOs.Responses;
using Testeger.Infra.UnitOfWork;
using Testeger.Application.Exceptions;
using DomainTestRequest = Testeger.Domain.Models.Entities.TestRequest;

namespace Testeger.Application.Services.TestRequest;

public class TestRequestService : BaseService, ITestRequestService
{
    public TestRequestService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request)
    {
        var testRequest = _mapper.Map<DomainTestRequest>(request);

        await ValidateProjectExistence(request.ProjectId);

        testRequest.Id = GenerateGuid();
        testRequest.CreatedDate = DateTime.Now;
        testRequest.Number = await GetTestRequestNumber(request.ProjectId);

        await _unitOfWork.TestRequestRepository.AddAsync(testRequest);
        await _unitOfWork.CompleteAsync();

        var response = _mapper.Map<CreateTestRequestResponse>(testRequest);

        return response;
    }

    private async Task ValidateProjectExistence(string projectId)
    {
        _ = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId) ?? throw new NotFoundException($"You must inform an existing project. Project with id {projectId} not found");
    }

    private async Task<int> GetTestRequestNumber(string projectId)
    {
        return await _unitOfWork.TestRequestRepository.GetNextNumberAsync(projectId);
    }
}
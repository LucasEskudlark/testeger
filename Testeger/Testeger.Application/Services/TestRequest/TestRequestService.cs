﻿using AutoMapper;
using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.DTOs.Responses.TestRequest;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
using DomainTestRequest = Testeger.Domain.Models.Entities.TestRequest;

namespace Testeger.Application.Services.TestRequest;

public class TestRequestService : BaseService, ITestRequestService
{
    public TestRequestService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request)
    {
        await ValidateProjectExistence(request.ProjectId);

        var testRequest = _mapper.Map<DomainTestRequest>(request);

        testRequest.Id = GenerateGuid();
        testRequest.CreatedDate = DateTime.Now;
        testRequest.Number = await GetTestRequestNumber(request.ProjectId);

        var response = _mapper.Map<CreateTestRequestResponse>(testRequest);


        await _unitOfWork.TestRequestRepository.AddAsync(testRequest);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    public async Task<GetTestRequestResponse> GetTestRequestByIdAsync(string id)
    {
        var testRequest = await _unitOfWork.TestRequestRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"TestRequest with id {id} not found");

        var response = _mapper.Map<GetTestRequestResponse>(testRequest);

        return response;
    }

    public async Task<PagedResponse<GetTestRequestResponse>> GetAllTestRequestsPagedAsync(PagedRequest request)
    {
        var testRequests = await _unitOfWork.TestRequestRepository.GetAllPagedAsync(request.PageSize, request.PageNumber);

        var response = _mapper.Map<PagedResponse<GetTestRequestResponse>>(testRequests);

        return response;
    }

    public async Task DeleteTestRequestAsync(string id)
    {
        var testRequest = await _unitOfWork.TestRequestRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"TestRequest with id {id} not found");

        await _unitOfWork.TestRequestRepository.Delete(testRequest);
        await _unitOfWork.CompleteAsync();
    }

    private async Task ValidateProjectExistence(string projectId)
    {
        _ = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId)
            ?? throw new NotFoundException($"You must inform an existing project. Project with id {projectId} not found");
    }

    private async Task<int> GetTestRequestNumber(string projectId)
    {
        return await _unitOfWork.TestRequestRepository.GetNextNumberAsync(projectId);
    }
}

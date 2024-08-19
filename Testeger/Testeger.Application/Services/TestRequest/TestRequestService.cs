using AutoMapper;
using Testeger.Application.Exceptions;
using Testeger.Domain.Enumerations;
using Testeger.Domain.Models.ValueObjects;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Requests.UpdateTestRequest;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.TestRequest;
using DomainTestRequest = Testeger.Domain.Models.Entities.TestRequest;

namespace Testeger.Application.Services.TestRequest;

public class TestRequestService : BaseService, ITestRequestService
{
    private static readonly HashSet<string> IgnoredProperties = ["Id"];

    public TestRequestService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateTestRequestResponse> CreateTestRequestAsync(CreateTestRequestRequest request)
    {
        await ValidateProjectExistence(request.ProjectId);

        var testRequest = _mapper.Map<DomainTestRequest>(request);

        testRequest.Id = GenerateGuid();
        testRequest.CreatedDate = DateTime.Now;
        testRequest.Status = RequestStatus.Waiting;
        testRequest.Number = await GetTestRequestNumber(request.ProjectId);

        var history = GetRequestHistory(request.UserId, RequestStatus.None, RequestStatus.Waiting);

        testRequest.History.Add(history);

        var response = _mapper.Map<CreateTestRequestResponse>(testRequest);

        await _unitOfWork.TestRequestRepository.AddAsync(testRequest);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    public async Task<GetTestRequestResponse> GetTestRequestByIdAsync(string id)
    {
        var testRequest = await FindTestRequestByIdAsync(id);

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
        var testRequest = await FindTestRequestByIdAsync(id);

        await _unitOfWork.TestRequestRepository.Delete(testRequest);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<GetTestRequestResponse>> GetTestRequestsByProjectIdAsync(string projectId)
    {
        await ValidateProjectExistence(projectId);

        var testRequests = await _unitOfWork.TestRequestRepository.GetTestRequestsByProjectIdAsync(projectId);

        var response = _mapper.Map<IEnumerable<GetTestRequestResponse>>(testRequests);
        return response;
    }

    public async Task UpdateTestRequestAsync(UpdateTestRequestRequest request)
    {
        var existingEntity = await FindTestRequestByIdAsync(request.Id);

        var updatedProperties = UpdateEntityProperties(existingEntity, request);

        if (updatedProperties.Count != 0)
        {

            await _unitOfWork.CompleteAsync();
            return;
        }

        return;
    }

    private static List<string> UpdateEntityProperties(DomainTestRequest entity, UpdateTestRequestRequest request)
    {
        var updatedProperties = new List<string>();
        var dtoProperties = typeof(UpdateTestRequestRequest).GetProperties();
        var entityType = typeof(DomainTestRequest);

        foreach (var dtoProp in dtoProperties)
        {
            if (IgnoredProperties.Contains(dtoProp.Name))
                continue;

            var entityProp = entityType.GetProperty(dtoProp.Name);
            if (entityProp != null && entityProp.CanWrite)
            {
                var dtoValue = dtoProp.GetValue(request);
                var entityValue = entityProp.GetValue(entity);

                if (dtoProp.PropertyType.IsEnum && entityProp.PropertyType.IsEnum && dtoValue is not null)
                {
                    dtoValue = Enum.ToObject(entityProp.PropertyType, (int)dtoValue);
                }

                if (!object.Equals(dtoValue, entityValue))
                {
                    entityProp.SetValue(entity, dtoValue);
                    updatedProperties.Add(dtoProp.Name);
                }
            }
        }

        return updatedProperties;
    }

    private static object MapEnumValue(Type entityEnumType, object dtoValue)
    {
        return Enum.ToObject(entityEnumType, (int)dtoValue);
    }

    private async Task<DomainTestRequest> FindTestRequestByIdAsync(string id)
    {
        var testRequest = await _unitOfWork.TestRequestRepository.GetByIdAsync(id) ??
             throw new NotFoundException($"TestRequest with id {id} not found");

        return testRequest;
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

    private static TestRequestHistory GetRequestHistory(
        string userId,
        RequestStatus oldStatus,
        RequestStatus newStatus)
    {
        return new TestRequestHistory
        {
            ChangedByUserId = userId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedDate = DateTime.Now
        };
    }
}

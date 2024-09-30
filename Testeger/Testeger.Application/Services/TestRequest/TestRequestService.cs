using AutoMapper;
using Microsoft.Extensions.Options;
using Testeger.Application.Exceptions;
using Testeger.Application.Helpers;
using Testeger.Application.Services.Email;
using Testeger.Application.Services.User;
using Testeger.Application.Settings;
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

public class TestRequestService : BaseUpdateService<DomainTestRequest, UpdateTestRequestRequest>, ITestRequestService
{
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly ClientSettings _clientSettings;

    public TestRequestService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IUserService userService, IOptions<ClientSettings> clientSettings) : base(unitOfWork, mapper)
    {
        _emailService = emailService;
        _userService = userService;
        _clientSettings = clientSettings.Value;
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
        var testRequest = await FindEntityByIdAsync(id);

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
        var testRequest = await FindEntityByIdAsync(id);

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
        var existingEntity = await FindEntityByIdAsync(request.Id);

        var updatedProperties = UpdateEntityPropertiesAsync(existingEntity, request);

        if (updatedProperties.Count > 0)
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    protected override async void OnPropertyUpdated(DomainTestRequest entity, string propertyName, object oldValue, object newValue)
    {
        if (propertyName == "Status")
        {
            var history = GetRequestHistory(entity.CreatedByUserId, (RequestStatus)oldValue, (RequestStatus)newValue);
            entity.History.Add(history);

            if ((RequestStatus)newValue is RequestStatus.Completed)
            {
                entity.CompletedDate = DateTime.Now;
            }

            if ((RequestStatus)newValue is RequestStatus.FixingIssues)
            {
                var user = await _userService.GetUserByIdAsync(entity.CreatedByUserId);

                var body = EmailHelper.GetRequestNeedsFixingEmailBody(_clientSettings.BaseAddress, user.UserName, entity);
                var subject = EmailHelper.GetRequestNeedsFixingEmailSubject();

                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
        }
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

    protected override async Task<DomainTestRequest> FindEntityByIdAsync(string id)
    {
        var testRequest = await _unitOfWork.TestRequestRepository.GetByIdAsync(id) ??
             throw new NotFoundException($"TestRequest with id {id} not found");

        return testRequest;
    }
}

using AutoMapper;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses;
using Testeger.Shared.DTOs.Responses.Project;
using DomainProject = Testeger.Domain.Models.Entities.Project;

namespace Testeger.Application.Services.Project;

public class ProjectService : BaseService, IProjectService
{
    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CreateProjectResponse> CreateProject(CreateProjectRequest request)
    {
        var project = _mapper.Map<DomainProject>(request);

        project.Id = GenerateGuid();
        project.CreatedDate = DateTime.Now;

        var response = _mapper.Map<CreateProjectResponse>(project);

        await _unitOfWork.ProjectRepository.AddAsync(project);
        await _unitOfWork.CompleteAsync();

        return response;
    }

    public async Task<PagedResponse<GetProjectResponse>> GetAllProjectsAsync(PagedRequest request)
    {
        var projects = await _unitOfWork.ProjectRepository.GetAllPagedAsync(request.PageSize, request.PageNumber);

        var response = _mapper.Map<PagedResponse<GetProjectResponse>>(projects);

        return response;
    }

    public async Task<GetProjectResponse> GetProjectById(string id)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Project with id {id} not found");

        var response = _mapper.Map<GetProjectResponse>(project);

        return response;
    }

    public async Task DeleteProject(string id)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Could not delete project. Project with id {id} not found");

        await _unitOfWork.ProjectRepository.Delete(project);
        await _unitOfWork.CompleteAsync();
    }
}

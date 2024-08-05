using AutoMapper;
using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Responses;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;

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

        await _unitOfWork.ProjectRepository.AddAsync(project);
        await _unitOfWork.CompleteAsync();

        var response = _mapper.Map<CreateProjectResponse>(project);
        return response;
    }

    public async Task<GetProjectResponse> GetProjectById(string id)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Project with id {id} not found");

        var response = _mapper.Map<GetProjectResponse>(project);

        return response;
    }
}

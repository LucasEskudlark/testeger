using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Responses.Project;

namespace Testeger.Application.MappingProfiles;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<CreateProjectRequest, Project>()
            .ForMember(p => p.CreatedByUserId,
                x => x.MapFrom(r => r.UserId));
        CreateMap<Project, CreateProjectResponse>();
        CreateMap<Project, GetProjectResponse>();
    }
}

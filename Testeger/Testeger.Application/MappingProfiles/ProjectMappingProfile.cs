using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateProject;
using Testeger.Application.DTOs.Responses;
using Testeger.Domain.Models.Entities;

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

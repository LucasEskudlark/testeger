using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Common;

namespace Testeger.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserNameIdDto>()
            .ForMember(dto => dto.UserId, x => x.MapFrom(u => u.Id))
            .ForMember(dto => dto.Username, x => x.MapFrom(u => u.UserName));
    }
}

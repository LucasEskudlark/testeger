using AutoMapper;
using Testeger.Domain.Models.Pagination;
using Testeger.Shared.DTOs.Responses;

namespace Testeger.Application.MappingProfiles;

public class PaginationMappingProfile : Profile
{
    public PaginationMappingProfile()
    {
        CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
    }
}

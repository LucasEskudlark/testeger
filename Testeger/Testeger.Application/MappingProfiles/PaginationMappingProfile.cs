using AutoMapper;
using Testeger.Application.DTOs.Responses;
using Testeger.Domain.Models.Pagination;

namespace Testeger.Application.MappingProfiles;

public class PaginationMappingProfile : Profile
{
    public PaginationMappingProfile()
    {
        CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
    }
}

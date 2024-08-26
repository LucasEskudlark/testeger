using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Responses.Image;

namespace Testeger.Application.MappingProfiles;

public class ImageMappingProfile : Profile
{
    public ImageMappingProfile()
    {
        CreateMap<Image, GetImageResponse>();
    }
}

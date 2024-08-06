using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateTestRequest;
using Testeger.Application.DTOs.Responses.TestRequest;
using Testeger.Domain.Models.Entities;

namespace Testeger.Application.MappingProfiles;

public class TestRequestMappingProfile : Profile
{
    public TestRequestMappingProfile()
    {
        CreateMap<CreateTestRequestRequest, TestRequest>()
            .ForMember(tr => tr.CreatedByUserId,
                opt => opt.MapFrom(r => r.UserId));

        CreateMap<TestRequest, CreateTestRequestResponse>();
        
        CreateMap<TestRequest, GetTestRequestResponse>();
    }
}

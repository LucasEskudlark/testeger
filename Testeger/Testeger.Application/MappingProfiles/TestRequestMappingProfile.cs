using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Domain.Models.ValueObjects;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Responses.TestRequest;

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

        CreateMap<TestRequestHistory, TestRequestHistoryResponse>();
    }
}

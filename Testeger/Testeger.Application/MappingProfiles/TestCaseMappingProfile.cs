using AutoMapper;
using Testeger.Application.DTOs.Requests.Common;
using Testeger.Application.DTOs.Requests.CreateTestCase;
using Testeger.Application.DTOs.Responses.TestCase;
using Testeger.Domain.Models.Entities;
using Testeger.Domain.Models.ValueObjects;

namespace Testeger.Application.MappingProfiles;

public class TestCaseMappingProfile : Profile
{
    public TestCaseMappingProfile()
    {
        CreateMap<CreateTestCaseRequest, TestCase>()
            .ForMember(t => t.CreatedByUserId,
                opt => opt.MapFrom(r => r.UserId));
        CreateMap<TestCase, CreateTestCaseResponse>();

        CreateMap<TestCase, GetTestCaseResponse>();

        CreateMap<TestCaseDetailsRequest, TestCaseDetails>();
        CreateMap<TestCaseDetails, TestCaseDetailsResponse>();
    }
}

using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Domain.Models.ValueObjects;
using Testeger.Shared.DTOs.Requests.Common;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Responses.TestCase;

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

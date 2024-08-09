using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Application.MappingProfiles;

public class TestCaseResultMappingProfile : Profile
{
    public TestCaseResultMappingProfile()
    {
        CreateMap<CreateTestCaseResultRequest, TestCaseResult>();
        CreateMap<TestCaseResult, CreateTestCaseResultResponse>();
        CreateMap<TestCaseResult, GetTestCaseResultResponse>();
    }
}

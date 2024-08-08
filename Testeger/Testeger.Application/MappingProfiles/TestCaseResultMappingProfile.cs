using AutoMapper;
using Testeger.Application.DTOs.Requests.CreateTestCaseResult;
using Testeger.Application.DTOs.Responses.TestCaseResult;
using Testeger.Domain.Models.Entities;

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

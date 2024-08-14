using AutoMapper;
using Testeger.Domain.Models.Entities;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.FinishTestCaseResult;
using Testeger.Shared.DTOs.Requests.UpdateTestCaseResult;
using Testeger.Shared.DTOs.Responses.TestCaseResult;

namespace Testeger.Application.MappingProfiles;

public class TestCaseResultMappingProfile : Profile
{
    public TestCaseResultMappingProfile()
    {
        CreateMap<CreateTestCaseResultRequest, TestCaseResult>().ReverseMap();
        CreateMap<UpdateTestCaseResultRequest, TestCaseResult>().ReverseMap();
        CreateMap<FinishTestCaseResultRequest, TestCaseResult>();
        CreateMap<TestCaseResult, CreateTestCaseResultResponse>();
        CreateMap<TestCaseResult, GetTestCaseResultResponse>();
    }
}

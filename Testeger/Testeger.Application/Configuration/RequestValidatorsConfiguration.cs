using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testeger.Shared.DTOs.Requests.Authentication.Login;
using Testeger.Shared.DTOs.Requests.Authentication.Register;
using Testeger.Shared.DTOs.Requests.ConfirmInvitation;
using Testeger.Shared.DTOs.Requests.CreateProject;
using Testeger.Shared.DTOs.Requests.CreateTestCase;
using Testeger.Shared.DTOs.Requests.CreateTestCaseResult;
using Testeger.Shared.DTOs.Requests.CreateTestRequest;
using Testeger.Shared.DTOs.Requests.SendInvitation;

namespace Testeger.Application.Configuration;

public static class RequestValidatorsConfiguration
{
    public static void AddRequestValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
        services.AddScoped<IValidator<CreateTestRequestRequest>, CreateTestRequestRequestValidator>();
        services.AddScoped<IValidator<CreateTestCaseRequest>, CreateTestCaseRequestValidator>();
        services.AddScoped<IValidator<CreateTestCaseResultRequest>, CreateTestCaseResultRequestValidator>();
        services.AddScoped<IValidator<UserRegisterRequest>, UserRegisterRequestValidator>();
        services.AddScoped<IValidator<UserLoginRequest>, UserLoginRequestValidator>();
        services.AddScoped<IValidator<SendInvitationRequest>, SendInvitationRequestValidator>();
        services.AddScoped<IValidator<ConfirmInvitationRequest>, ConfirmInvitationRequestValidator>();
    }
}

namespace Testeger.Client.Services.Authorization;

public interface IPolicyValidatorService
{
    Task<bool> ValidatePolicyAsync(string policyName);
}

﻿using Testeger.Client.ViewModels.Authentication;

namespace Testeger.Client.Services.Authentication;

public interface ICustomAuthService
{
    Task<AuthResult> LoginAsync(LoginViewModel loginViewModel);
    Task<AuthResult> RegisterAsync(RegisterViewModel registerViewModel);
    Task<AuthResult> ReAuthenticateUserAsync();
}

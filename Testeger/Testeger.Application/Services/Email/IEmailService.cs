﻿namespace Testeger.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
}

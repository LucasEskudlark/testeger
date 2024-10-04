using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Testeger.Application.Settings;

namespace Testeger.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _stmpSettings;
    private readonly ISmtpClient _smtpClient;

    public EmailService(IOptions<SmtpSettings> stmpSettings, ISmtpClient smtpClient)
    {
        _stmpSettings = stmpSettings.Value;
        _smtpClient = smtpClient;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_stmpSettings.SenderName, _stmpSettings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = body
        };

        await _smtpClient.ConnectAsync(_stmpSettings.Server, _stmpSettings.Port, _stmpSettings.UseSsl);
        await _smtpClient.AuthenticateAsync(_stmpSettings.Username, _stmpSettings.Password);

        await _smtpClient.SendAsync(message);
        await _smtpClient.DisconnectAsync(true);
    }
}

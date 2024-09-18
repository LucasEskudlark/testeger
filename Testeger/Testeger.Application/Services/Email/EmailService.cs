using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Testeger.Application.Settings;

namespace Testeger.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _stmpSettings;

    public EmailService(IOptions<SmtpSettings> stmpSettings)
    {
        _stmpSettings = stmpSettings.Value;
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

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(_stmpSettings.Server, _stmpSettings.Port, _stmpSettings.UseSsl);
        await smtp.AuthenticateAsync(_stmpSettings.Username, _stmpSettings.Password);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}

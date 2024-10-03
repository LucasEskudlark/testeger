using MailKit.Net.Smtp;
using MimeKit;

namespace UnitTests.Application.Services.Email;

public class EmailServiceTests
{
    private readonly SmtpSettings _smtpSettings;
    private readonly EmailService _emailService;
    private readonly Mock<ISmtpClient> _smtpClient;
    private const string FakeEmailSubject = "Subject";
    private const string FakeEmail = "email@email.com";
    private const string FakeEmailBody = $@"
            <html>
            <body>
                <p>Body</p>
            </body>
            </html>";

    public EmailServiceTests()
    {
        _smtpClient = new Mock<ISmtpClient>();

        _smtpSettings = SetupSmtpSettings();
        IOptions<SmtpSettings> options = Options.Create(_smtpSettings);

        _emailService = new(options, _smtpClient.Object);
    }

    [Fact]
    public async Task SendEmailAsync_GivenValidParameters_ShouldSendEmailAsExpected()
    {
        await _emailService.SendEmailAsync(FakeEmail, FakeEmailSubject, FakeEmailBody);

        _smtpClient.Verify(smtp => smtp.ConnectAsync(
            _smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl, It.IsAny<CancellationToken>()), Times.Once);

        _smtpClient.Verify(smtp => smtp.AuthenticateAsync(
            _smtpSettings.Username, _smtpSettings.Password, It.IsAny<CancellationToken>()), Times.Once);

        _smtpClient.Verify(smtp => smtp.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), default), Times.Once);

        _smtpClient.Verify(smtp => smtp.DisconnectAsync(true, It.IsAny<CancellationToken>()), Times.Once);
    }

    private static SmtpSettings SetupSmtpSettings()
    {
        return new()
        {
            Server = "Server",
            Port = 10,
            UseSsl = true,
            Username = "Username",
            Password = "Password",
            SenderName = "SenderName",
            SenderEmail = "SenderEmail"
        };
    }
}

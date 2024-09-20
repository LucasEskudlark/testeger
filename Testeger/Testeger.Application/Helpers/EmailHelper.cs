namespace Testeger.Application.Helpers;

public static class EmailHelper
{
    public static string GetProjectInvitationEmailBody(string token)
    {
        string link = $"https://localhost:49851/confirm-invitation?token={Uri.EscapeDataString(token)}";
        string body = $@"
            <html>
            <body>
                <p>You have been invited to join a project. Please confirm your invitation by clicking the link below:</p>
                <p><a href=""{link}"">Confirm Invitation</a></p>
                <br/>
                <p>Best regards,<br/>Testeger</p>
            </body>
            </html>";

        return body;
    }

    public static string GetProjectInvitationEmailSubject()
    {
        string subject = "You've been invited to join a project!";

        return subject;
    }
}

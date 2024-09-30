using Testeger.Application.Extensions;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Application.Helpers;

public static class EmailHelper
{
    public static string GetProjectInvitationEmailBody(string token, string baseUrl, RoleType roleType)
    {
        string link = $"{baseUrl}/confirm-invitation?token={Uri.EscapeDataString(token)}";
        string body = $@"
            <html>
            <body>
                <p>You have been invited to join a project as a {roleType.GetDescription()}. Please confirm your invitation by clicking the link below:</p>
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

using Newtonsoft.Json.Linq;
using Testeger.Application.Extensions;
using Testeger.Domain.Models.Entities;
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

    public static string GetRequestNeedsFixingEmailBody(string baseUrl, string userName, TestRequest request)
    {
        string link = $"{baseUrl}/project/{request.ProjectId}/test-request/{request.Id}";

        string body = $@"
            <html>
            <body>
                <p>What's up {userName}!</p>
                <p>It looks like a request you created was tested and the result was not successful. The Q.A team will be waiting for you to fix it before another test.</p>
                <br/>
                <p>Detailed info below: </p> 
                <p>Title: {request.Title} </p> 
                <p>Link to request: <a href=""{link}"">Click here</a></p>
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

    public static string GetRequestNeedsFixingEmailSubject()
    {
        string subject = "It seems like a request you created failed a test :(";

        return subject;
    }
}

using RadzenNotificationService = Radzen.NotificationService;
using RadzenNotificationMessage = Radzen.NotificationMessage;
using Radzen;

namespace Testeger.Client.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly RadzenNotificationService _radzenNotificationService;
    private const double NotificationDuration = 4000;

    public NotificationService(RadzenNotificationService radzenNotificationService)
    {
        _radzenNotificationService = radzenNotificationService;
    }

    public void ShowFailNotification(string summary, string detail)
    {
        var notification = GetNotificationMessage(NotificationSeverity.Error, summary, detail);
        _radzenNotificationService.Notify(notification);
    }

    public void ShowSuccessNotification(string summary, string detail)
    {
        var notification = GetNotificationMessage(NotificationSeverity.Success, summary, detail);
        _radzenNotificationService.Notify(notification);
    }

    private static RadzenNotificationMessage GetNotificationMessage(NotificationSeverity severity, string summary, string detail)
    {
        return new RadzenNotificationMessage
        {
            Severity = severity,
            Summary = summary,
            Detail = detail,
            Duration = NotificationDuration
        };
    }
}

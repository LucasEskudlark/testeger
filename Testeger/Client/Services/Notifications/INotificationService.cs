namespace Testeger.Client.Services.Notifications;

public interface INotificationService
{
    void ShowSuccessNotification(string summary, string detail);
    void ShowFailNotification(string summary, string detail);
}

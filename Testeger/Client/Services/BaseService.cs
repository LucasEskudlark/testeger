using Testeger.Client.Services.Notifications;

namespace Testeger.Client.Services;

public class BaseService
{
    protected readonly HttpClient _httpClient;
    protected readonly INotificationService _notificationService;

    public BaseService(HttpClient httpClient, INotificationService notificationService)
    {
        _httpClient = httpClient;
        _notificationService = notificationService;
    }
}

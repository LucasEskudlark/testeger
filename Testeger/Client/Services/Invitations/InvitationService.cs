﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Testeger.Client.Services.Notifications;
using Testeger.Client.ViewModels.Invitations;
using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.Services.Invitations;

public class InvitationService : BaseService, IInvitationService
{
    private const string BaseAddress = "api/invitations";

    public InvitationService(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService)
    {
    }

    public async Task SendProjectInvitationAsync(IEnumerable<UserInvitationViewModel> users, string projectId)
    {
        var address = BaseAddress + "/send";

        var request = new InvitationViewModel
        {
            Users = users,
            ProjectId = projectId
        };

        var response = await _httpClient.PostAsJsonAsync(address, request);

        if(!response.IsSuccessStatusCode)
        {
            _notificationService.ShowFailNotification("Error", "Could not invite users.");
            return;
        }

        _notificationService.ShowSuccessNotification("Success", "Users successfully invited.");
    }
}

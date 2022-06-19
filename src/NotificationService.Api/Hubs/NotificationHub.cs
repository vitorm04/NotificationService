using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Api.Hubs
{
    public class NotificationHub : Hub<INotificationHubClient>
    {
        public async Task SendNewNotification(string message) => await Clients.Others.SendNewNotification(message);

    }

    public interface INotificationHubClient
    {
        Task SendNewNotification(string message);
    }
}

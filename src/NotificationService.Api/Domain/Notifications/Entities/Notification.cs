namespace NotificationService.Api.Domain.Notifications.Entities
{
    public class Notification
    {
        public Notification(string message)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(message));

            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Message = message;
        }

        public Guid Id { get; }
        public string Message { get; }
        public DateTime CreatedAt { get; }
    }
}

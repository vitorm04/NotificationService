using NotificationService.Api.Kafka.Events;

namespace NotificationService.Api
{
    public class Worker : IHostedService
    {
        private readonly INotificationConsumer _notificationConsumer;
        private readonly ILogger<Worker> _logger;

        public Worker(INotificationConsumer notificationConsumer, ILogger<Worker> logger)
        {
            _notificationConsumer = notificationConsumer;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(() =>
            {
                _logger.LogInformation("StartAsync");
                _notificationConsumer.ConsumeNotification();
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync");
            return Task.CompletedTask;
        }
    }
}

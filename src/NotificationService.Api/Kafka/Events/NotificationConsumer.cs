using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NotificationService.Api.Hubs;

namespace NotificationService.Api.Kafka.Events
{
    public sealed class NotificationConsumer : KafkaCommonNotification, INotificationConsumer
    {
        private readonly IHubContext<NotificationHub, INotificationHubClient> _chatHubContext;
        private readonly ILogger<NotificationConsumer> _logger;

        public NotificationConsumer(IOptions<KafkaConnectionConfiguration> optionsKafkaConfig,
                                    ILogger<NotificationConsumer> logger, IHubContext<NotificationHub,
                                    INotificationHubClient> chatHubContext) : base(optionsKafkaConfig.Value)
        {
            _logger = logger;
            _chatHubContext = chatHubContext;
        }

        public void ConsumeNotification()
        {
            var config = CreateConnection<ConsumerConfig>();
            config.GroupId = "NotificationService";

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(NOTIFICATION_TOPIC);

            _logger.LogInformation("Listening");

            while (true)
            {
                _logger.LogInformation("New Notification");

                var consumeResult = consumer.Consume();
                if (consumeResult is null) continue;

                _chatHubContext.Clients.All.SendNewNotification(consumeResult.Message.Value);
            }
        }
    }

    public interface INotificationConsumer
    {
        void ConsumeNotification();
    }
}

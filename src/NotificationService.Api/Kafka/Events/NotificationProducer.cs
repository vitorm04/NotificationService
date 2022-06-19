using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace NotificationService.Api.Kafka.Events
{
    public sealed class NotificationProducer : KafkaCommonNotification, INotificationProducer
    {
        public NotificationProducer(IOptions<KafkaConnectionConfiguration> optionsKafkaConfig) : base(optionsKafkaConfig.Value)
        {
        }

        public async Task ProcuceNewNotificationAsync(string message, CancellationToken cancellation)
        {
            var config = CreateConnection<ProducerConfig>();
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var @event = new Message<Null, string> { Value = message };
            var result = await producer.ProduceAsync(NOTIFICATION_TOPIC, @event, cancellation);
        }
    }

    public interface INotificationProducer
    {
        Task ProcuceNewNotificationAsync(string message, CancellationToken cancellation);
    }
}

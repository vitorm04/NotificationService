using Confluent.Kafka;

namespace NotificationService.Api.Kafka
{
    public record KafkaConnectionConfiguration
    {
        public string? BootstrapServers { get; init; }
        public string? ClientId { get; init; }
        public string? Password { get; init; }
        public string? UserName { get; init; }
        public SecurityProtocol SecurityProtocol { get; init; }
        public SaslMechanism SaslMechanism { get; init; }
    }
}

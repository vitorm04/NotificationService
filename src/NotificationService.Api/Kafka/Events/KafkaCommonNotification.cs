using Confluent.Kafka;

namespace NotificationService.Api.Kafka.Events
{
    public abstract class KafkaCommonNotification
    {
        private readonly KafkaConnectionConfiguration _kafkaConfiguration;
        protected const string NOTIFICATION_TOPIC = "notification";

        protected KafkaCommonNotification(KafkaConnectionConfiguration kafkaConfiguration)
        {
            _kafkaConfiguration = kafkaConfiguration;
        }

        protected T CreateConnection<T>() where T: ClientConfig, new()
        {
            return new T
            {
                BootstrapServers = _kafkaConfiguration.BootstrapServers,
                ClientId = _kafkaConfiguration.ClientId,
                SaslPassword = _kafkaConfiguration.Password,
                SaslUsername = _kafkaConfiguration.UserName,
                SecurityProtocol = _kafkaConfiguration.SecurityProtocol,
                SaslMechanism = _kafkaConfiguration.SaslMechanism,
            };
        }
    }
}

using MediatR;
using NotificationService.Api.Kafka.Events;

namespace NotificationService.Api.Application.UseCases.SendNewNotification
{
    internal class SendNewNotificationUseCase : IRequestHandler<SendNewNotificationInput, SendNewNotificationOutput>
    {
        private readonly INotificationProducer _notificationProducer;

        public SendNewNotificationUseCase(INotificationProducer notificationProducer)
        {
            _notificationProducer = notificationProducer;
        }

        public async Task<SendNewNotificationOutput> Handle(SendNewNotificationInput request, CancellationToken cancellationToken)
        {
            await _notificationProducer.ProcuceNewNotificationAsync(request.Message, cancellationToken);
            return new SendNewNotificationOutput(Guid.NewGuid());
        }
    }

    public record SendNewNotificationInput : IRequest<SendNewNotificationOutput>
    {
        public SendNewNotificationInput(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public record SendNewNotificationOutput
    {
        public SendNewNotificationOutput(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        public Guid NotificationId { get; }
    }
}

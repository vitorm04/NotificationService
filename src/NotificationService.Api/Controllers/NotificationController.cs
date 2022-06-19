using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Api.Application.UseCases.SendNewNotification;
using NotificationService.Api.Domain.Notifications.Entities;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Api.Controllers
{

    [Route("api/notifications")]
    public sealed class NotificationController : ControllerBase
    {

        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendNewNotification([FromBody][Required] SendNewNotificationInput notification, CancellationToken cancellationToken = default)
        {
            var output = await _mediator.Send(notification, cancellationToken);
            return Created(String.Empty, output);
        }
    }
}

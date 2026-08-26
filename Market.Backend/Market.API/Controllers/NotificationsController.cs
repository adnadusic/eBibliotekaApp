using Market.Application.Modules.Notifications.Commands.SetReadStatus;
using Market.Application.Modules.Notifications.Queries.GetMyNotifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers;

[ApiController]
[Route("api/notifications")]
[Authorize]
public sealed class NotificationsController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetMyNotificationsItemDto>>> GetMyNotifications(
        CancellationToken ct)
    {
        var result = await mediator.Send(
            new GetMyNotificationsQuery(),
            ct);

        return Ok(result);
    }

    [HttpPut("read-status")]
    public async Task<IActionResult> SetReadStatus(
        [FromBody] SetNotificationReadStatusCommand command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);

        return NoContent();
    }
}
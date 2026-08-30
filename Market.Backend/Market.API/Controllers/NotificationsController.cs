using Market.Application.Common;
using Market.Application.Modules.Notifications.Commands.SetPriority;
using Market.Application.Modules.Notifications.Commands.SetReadStatus;
using Market.Application.Modules.Notifications.Queries.GetMyNotificationSettings;
using Market.Application.Modules.Notifications.Queries.GetMyNotifications;
using Market.Domain.Enums;
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
    public async Task<ActionResult<PageResult<GetMyNotificationsItemDto>>> GetMyNotifications(
        [FromQuery] NotificationType? type,
        [FromQuery] bool? isRead,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(
            new GetMyNotificationsQuery
            {
                Type = type,
                IsRead = isRead,
                Paging = new PageRequest
                {
                    Page = Math.Max(1, page),
                    PageSize = pageSize
                }
            },
            ct);

        return Ok(result);
    }

    [HttpGet("settings")]
    public async Task<ActionResult<List<GetMyNotificationSettingsItemDto>>> GetMySettings(
        CancellationToken ct)
    {
        var result = await mediator.Send(
            new GetMyNotificationSettingsQuery(),
            ct);

        return Ok(result);
    }

    [HttpPut("priority")]
    public async Task<IActionResult> SetPriority(
        [FromBody] SetNotificationPriorityCommand command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);

        return NoContent();
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
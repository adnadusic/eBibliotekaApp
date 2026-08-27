using Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers;

[ApiController]
[Route("api/audit-trail")]
[Authorize]
public sealed class AuditTrailController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetAuditLogsItemDto>>> GetAuditLogs(
        [FromQuery] string? entityName,
        [FromQuery] string? action,
        CancellationToken ct)
    {
        var result = await mediator.Send(
            new GetAuditLogsQuery
            {
                EntityName = entityName,
                Action = action
            },
            ct);

        return Ok(result);
    }
}
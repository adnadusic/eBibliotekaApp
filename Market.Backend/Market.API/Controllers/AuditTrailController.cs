using Market.API.Authorization;
using Market.Application.Common;
using Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers;

[ApiController]
[Route("api/audit-trail")]
[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
public sealed class AuditTrailController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PageResult<GetAuditLogsItemDto>>> GetAuditLogs(
        [FromQuery] string? entityName,
        [FromQuery] string? action,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(
            new GetAuditLogsQuery
            {
                EntityName = entityName,
                Action = action,
                Paging = new PageRequest
                {
                    Page = Math.Max(1, page),
                    PageSize = pageSize
                }
            },
            ct);

        return Ok(result);
    }
}
namespace Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;

public sealed class GetAuditLogsQueryHandler(
    IAppDbContext ctx)
    : IRequestHandler<
        GetAuditLogsQuery,
        PageResult<GetAuditLogsItemDto>>
{
    public async Task<PageResult<GetAuditLogsItemDto>> Handle(
        GetAuditLogsQuery request,
        CancellationToken ct)
    {
        var query = ctx.AuditLogs
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.EntityName))
        {
            query = query.Where(x =>
                x.EntityName == request.EntityName);
        }

        if (!string.IsNullOrWhiteSpace(request.Action))
        {
            query = query.Where(x =>
                x.Action == request.Action);
        }

        var projectedQuery = query
            .OrderByDescending(x => x.ChangedAtUtc)
            .ThenByDescending(x => x.Id)
            .Select(x => new GetAuditLogsItemDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserEmail = x.UserEmail,
                EntityName = x.EntityName,
                EntityId = x.EntityId,
                Action = x.Action,
                OldValues = x.OldValues,
                NewValues = x.NewValues,
                ChangedAtUtc = x.ChangedAtUtc
            });

        return await PageResult<GetAuditLogsItemDto>
            .FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}
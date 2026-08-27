namespace Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;

public sealed class GetAuditLogsQueryHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<
        GetAuditLogsQuery,
        List<GetAuditLogsItemDto>>
{
    public async Task<List<GetAuditLogsItemDto>> Handle(
        GetAuditLogsQuery request,
        CancellationToken ct)
    {
        if (!currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only administrators can view the audit trail.");
        }

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

        return await query
            .OrderByDescending(x => x.ChangedAtUtc)
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
            })
            .ToListAsync(ct);
    }
}
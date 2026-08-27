namespace Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;

public sealed class GetAuditLogsQuery
    : IRequest<List<GetAuditLogsItemDto>>
{
    public string? EntityName { get; init; }

    public string? Action { get; init; }
}
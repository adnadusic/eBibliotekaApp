namespace Market.Application.Modules.AuditTrail.Queries.GetAuditLogs;

public sealed class GetAuditLogsItemDto
{
    public int Id { get; init; }

    public int? UserId { get; init; }

    public string? UserEmail { get; init; }

    public string EntityName { get; init; } = string.Empty;

    public string? EntityId { get; init; }

    public string Action { get; init; } = string.Empty;

    public string? OldValues { get; init; }

    public string? NewValues { get; init; }

    public DateTime ChangedAtUtc { get; init; }
}
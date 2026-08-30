using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Queries.GetMyNotifications;

public sealed class GetMyNotificationsItemDto
{
    public int Id { get; init; }
    public NotificationType Type { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public DateTime SentAt { get; init; }
    public bool IsRead { get; init; }
    public DateTime? ReadAt { get; init; }
}
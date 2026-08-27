using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Queries.GetMyNotificationSettings;

public sealed class GetMyNotificationSettingsItemDto
{
    public NotificationType Type { get; init; }

    public bool IsPriority { get; init; }
}
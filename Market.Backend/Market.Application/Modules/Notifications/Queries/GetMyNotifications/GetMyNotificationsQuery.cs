using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Queries.GetMyNotifications;

public sealed class GetMyNotificationsQuery
    : BasePagedQuery<GetMyNotificationsItemDto>
{
    public NotificationType? Type { get; init; }

    public bool? IsRead { get; init; }
}
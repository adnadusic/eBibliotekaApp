using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Queries.GetMyNotifications;

public sealed class GetMyNotificationsQuery
    : IRequest<List<GetMyNotificationsItemDto>>
{
    public NotificationType? Type { get; init; }

    public bool? IsRead { get; init; }
}
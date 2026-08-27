using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Commands.SetPriority;

public sealed class SetNotificationPriorityCommand : IRequest
{
    public NotificationType Type { get; init; }

    public bool IsPriority { get; init; }
}
namespace Market.Application.Modules.Notifications.Commands.SetReadStatus;

public sealed class SetNotificationReadStatusCommand : IRequest
{
    public int NotificationId { get; init; }
    public bool IsRead { get; init; }
}
namespace Market.Application.Modules.Notifications.Commands.SetReadStatus;

public sealed class SetNotificationReadStatusCommandValidator
    : AbstractValidator<SetNotificationReadStatusCommand>
{
    public SetNotificationReadStatusCommandValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0)
            .WithMessage(
                "Notification identifier must be greater than 0.");
    }
}
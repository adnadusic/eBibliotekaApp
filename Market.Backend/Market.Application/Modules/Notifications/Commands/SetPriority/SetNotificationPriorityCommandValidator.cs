namespace Market.Application.Modules.Notifications.Commands.SetPriority;

public sealed class SetNotificationPriorityCommandValidator
    : AbstractValidator<SetNotificationPriorityCommand>
{
    public SetNotificationPriorityCommandValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum();
    }
}
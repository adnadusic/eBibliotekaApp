using Market.Domain.Entities.Catalog;

namespace Market.Application.Modules.Notifications.Commands.SetPriority;

public sealed class SetNotificationPriorityCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<SetNotificationPriorityCommand>
{
    public async Task Handle(
        SetNotificationPriorityCommand request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var setting = await ctx.NotificationSettings
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.Type == request.Type,
                ct);

        if (setting is null)
        {
            setting = new NotificationSetting
            {
                UserId = userId,
                Type = request.Type,
                IsPriority = request.IsPriority
            };

            ctx.NotificationSettings.Add(setting);
        }
        else
        {
            setting.IsPriority = request.IsPriority;
            setting.IsDeleted = false;
        }

        await ctx.SaveChangesAsync(ct);
    }
}
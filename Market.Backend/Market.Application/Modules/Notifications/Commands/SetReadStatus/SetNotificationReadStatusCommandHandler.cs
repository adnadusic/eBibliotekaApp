namespace Market.Application.Modules.Notifications.Commands.SetReadStatus;

public sealed class SetNotificationReadStatusCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<SetNotificationReadStatusCommand>
{
    public async Task Handle(
        SetNotificationReadStatusCommand request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var notification = await ctx.Obavijesti
            .FirstOrDefaultAsync(
                x =>
                    x.Id == request.NotificationId &&
                    x.UserId == userId &&
                    !x.IsDeleted,
                ct);

        if (notification is null)
        {
            throw new MarketNotFoundException(
                "Notification was not found.");
        }

        notification.Procitano = request.IsRead;
        notification.DatumCitanja =
            request.IsRead ? DateTime.UtcNow : null;

        await ctx.SaveChangesAsync(ct);
    }
}
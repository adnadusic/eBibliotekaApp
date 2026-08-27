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

        var setting = await ctx.PostavkeObavijesti
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.Tip == request.Type,
                ct);

        if (setting is null)
        {
            setting = new PostavkaObavijesti
            {
                UserId = userId,
                Tip = request.Type,
                Prioritetna = request.IsPriority
            };

            ctx.PostavkeObavijesti.Add(setting);
        }
        else
        {
            setting.Prioritetna = request.IsPriority;
            setting.IsDeleted = false;
        }

        await ctx.SaveChangesAsync(ct);
    }
}
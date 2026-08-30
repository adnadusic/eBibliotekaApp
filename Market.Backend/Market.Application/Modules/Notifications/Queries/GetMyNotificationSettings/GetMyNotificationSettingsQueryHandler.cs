using Market.Domain.Enums;

namespace Market.Application.Modules.Notifications.Queries.GetMyNotificationSettings;

public sealed class GetMyNotificationSettingsQueryHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<
        GetMyNotificationSettingsQuery,
        List<GetMyNotificationSettingsItemDto>>
{
    public async Task<List<GetMyNotificationSettingsItemDto>> Handle(
        GetMyNotificationSettingsQuery request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var savedSettings = await ctx.NotificationSettings
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                !x.IsDeleted)
            .ToListAsync(ct);

        return Enum
            .GetValues<NotificationType>()
            .Select(type =>
            {
                var setting = savedSettings
                    .FirstOrDefault(x => x.Type == type);

                return new GetMyNotificationSettingsItemDto
                {
                    Type = type,
                    IsPriority = setting?.IsPriority ?? false
                };
            })
            .ToList();
    }
}
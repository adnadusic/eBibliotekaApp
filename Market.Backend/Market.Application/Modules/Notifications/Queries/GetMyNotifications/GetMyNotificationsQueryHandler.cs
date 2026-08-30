namespace Market.Application.Modules.Notifications.Queries.GetMyNotifications;

public sealed class GetMyNotificationsQueryHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<
        GetMyNotificationsQuery,
        List<GetMyNotificationsItemDto>>
{
    public async Task<List<GetMyNotificationsItemDto>> Handle(
        GetMyNotificationsQuery request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var query = ctx.Notifications
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (request.Type.HasValue)
        {
            query = query.Where(x =>
                x.Type == request.Type.Value);
        }

        if (request.IsRead.HasValue)
        {
            query = query.Where(x =>
                x.IsRead == request.IsRead.Value);
        }

        return await query
            .OrderByDescending(x => x.SentAt)
            .Select(x => new GetMyNotificationsItemDto
            {
                Id = x.Id,
                Type = x.Type,
                Title = x.Title,
                Message = x.Message,
                SentAt = x.SentAt,
                IsRead = x.IsRead,
                ReadAt = x.ReadAt
            })
            .ToListAsync(ct);
    }
}
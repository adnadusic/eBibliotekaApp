namespace Market.Application.Modules.Notifications.Queries.GetMyNotifications;

public sealed class GetMyNotificationsQueryHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<
        GetMyNotificationsQuery,
        PageResult<GetMyNotificationsItemDto>>
{
    public async Task<PageResult<GetMyNotificationsItemDto>> Handle(
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

        var projectedQuery = query
            .OrderByDescending(x => x.SentAt)
            .ThenByDescending(x => x.Id)
            .Select(x => new GetMyNotificationsItemDto
            {
                Id = x.Id,
                Type = x.Type,
                Title = x.Title,
                Message = x.Message,
                SentAt = x.SentAt,
                IsRead = x.IsRead,
                ReadAt = x.ReadAt
            });

        return await PageResult<GetMyNotificationsItemDto>
            .FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}
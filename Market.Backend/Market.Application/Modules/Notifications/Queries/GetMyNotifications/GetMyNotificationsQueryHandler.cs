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

        var query = ctx.Obavijesti
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (request.Type.HasValue)
        {
            query = query.Where(x =>
                x.Tip == request.Type.Value);
        }

        if (request.IsRead.HasValue)
        {
            query = query.Where(x =>
                x.Procitano == request.IsRead.Value);
        }

        return await query
            .OrderByDescending(x => x.DatumSlanja)
            .Select(x => new GetMyNotificationsItemDto
            {
                Id = x.Id,
                Type = x.Tip,
                Title = x.Naslov,
                Message = x.Poruka,
                SentAt = x.DatumSlanja,
                IsRead = x.Procitano,
                ReadAt = x.DatumCitanja
            })
            .ToListAsync(ct);
    }
}
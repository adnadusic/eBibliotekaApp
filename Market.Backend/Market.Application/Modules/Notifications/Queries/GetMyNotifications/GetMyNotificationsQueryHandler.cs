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

        return await ctx.Obavijesti
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                !x.IsDeleted)
            .OrderByDescending(x => x.DatumSlanja)
            .Select(x => new GetMyNotificationsItemDto
            {
                Id = x.Id,
                Type = x.Tip,
                Title = x.Naslov,
                Message = x.Poruka,
                SentAt = x.DatumSlanja,
                IsRead = x.Procitano ?? false,
                ReadAt = x.DatumCitanja,
                RelatedId = x.VezanoZaId,
                RelatedType = x.VezanoZaTip
            })
            .ToListAsync(ct);
    }
}
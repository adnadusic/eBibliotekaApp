namespace Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

public sealed class GetReviewsByBookQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetReviewsByBookQuery, List<GetReviewsByBookItemDto>>
{
    public async Task<List<GetReviewsByBookItemDto>> Handle(
        GetReviewsByBookQuery request,
        CancellationToken ct)
    {
        var bookExists = await ctx.Knjige
            .AnyAsync(
                x => x.Id == request.BookId && !x.IsDeleted,
                ct);

        if (!bookExists)
        {
            throw new MarketNotFoundException("Book was not found.");
        }

        return await ctx.Recenzije
            .AsNoTracking()
            .Where(x =>
                x.BookId == request.BookId &&
                !x.IsDeleted)
            .OrderByDescending(x => x.Datum)
            .Select(x => new GetReviewsByBookItemDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName = x.User.FirstName + " " + x.User.LastName,
                Rating = x.Ocjena,
                Title = x.Naslov,
                Comment = x.Komentar,
                Date = x.Datum,
                HelpfulCount = x.BrojHelpful,
                UnhelpfulCount = x.BrojUnhelpful
            })
            .ToListAsync(ct);
    }
}
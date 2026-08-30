namespace Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

public sealed class GetReviewsByBookQueryHandler(IAppDbContext ctx)
    : IRequestHandler<
        GetReviewsByBookQuery,
        PageResult<GetReviewsByBookItemDto>>
{
    public async Task<PageResult<GetReviewsByBookItemDto>> Handle(
        GetReviewsByBookQuery request,
        CancellationToken ct)
    {
        var bookExists = await ctx.Books
            .AnyAsync(
                x => x.Id == request.BookId && !x.IsDeleted,
                ct);

        if (!bookExists)
        {
            throw new MarketNotFoundException("Book was not found.");
        }

        var projectedQuery = ctx.Reviews
            .AsNoTracking()
            .Where(x =>
                x.BookId == request.BookId &&
                !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Select(x => new GetReviewsByBookItemDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName = x.User.FirstName + " " + x.User.LastName,
                Rating = x.Rating,
                Title = x.Title,
                Comment = x.Comment,
                Date = x.CreatedAt,
                HelpfulCount = x.HelpfulCount,
                UnhelpfulCount = x.UnhelpfulCount
            });

        return await PageResult<GetReviewsByBookItemDto>
            .FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}
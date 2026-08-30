using Market.Application.Modules.Catalog.Books.Queries.GetById;

public sealed class GetBookByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetBookByIdQuery, GetBookByIdDto>
{
    public async Task<GetBookByIdDto> Handle(
        GetBookByIdQuery request,
        CancellationToken ct)
    {
        var book = await ctx.Books
            .AsNoTracking()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new GetBookByIdDto
            {
                Id = x.Id,
                Title = x.Title,
                Isbn = x.Isbn,
                PublicationYear = x.PublicationYear,
                PageCount = x.PageCount,
                LanguageId = x.LanguageId,
                PublisherId = x.PublisherId,
                Description = x.Description,
                CoverImage = x.CoverImage,
                TotalCopies = x.TotalCopies,
                AvailableCopies = x.AvailableCopies,
                AverageRating = x.AverageRating,
                RatingCount = x.RatingCount,
                ViewCount = x.ViewCount,
                AddedAt = x.AddedAt,
                AuthorIds = x.Authors
                    .Select(a => a.AuthorId)
                    .ToList(),
                GenreIds = x.Genres
                    .Select(g => g.GenreId)
                    .ToList()
            })
            .FirstOrDefaultAsync(ct)
            ?? throw new MarketNotFoundException("Book was not found.");

        return book;
    }
}
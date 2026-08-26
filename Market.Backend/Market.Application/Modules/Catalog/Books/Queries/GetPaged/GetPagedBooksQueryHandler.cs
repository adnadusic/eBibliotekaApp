using Market.Application.Modules.Catalog.Books.Queries.GetPaged;

public sealed class GetPagedBooksQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetPagedBooksQuery, PageResult<GetPagedBooksItemDto>>
{
    public async Task<PageResult<GetPagedBooksItemDto>> Handle(
        GetPagedBooksQuery request,
        CancellationToken ct)
    {
        var query = ctx.Knjige
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            var title = request.Title.Trim();

            query = query.Where(x => x.Naslov.Contains(title));
        }

        if (!string.IsNullOrWhiteSpace(request.Isbn))
        {
            var isbn = request.Isbn.Trim();

            query = query.Where(x => x.Isbn.Contains(isbn));
        }

        if (request.AuthorId.HasValue)
        {
            query = query.Where(x =>
                x.Autori.Any(a => a.AuthorId == request.AuthorId.Value));
        }

        if (request.GenreId.HasValue)
        {
            query = query.Where(x =>
                x.Zanrovi.Any(g => g.GenreId == request.GenreId.Value));
        }

        if (request.LanguageId.HasValue)
        {
            query = query.Where(x =>
                x.JezikId == request.LanguageId.Value);
        }

        var sortBy = request.SortBy.Trim().ToLowerInvariant();

        var descending = request.SortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = sortBy switch
        {
            "isbn" => descending
                ? query.OrderByDescending(x => x.Isbn).ThenBy(x => x.Id)
                : query.OrderBy(x => x.Isbn).ThenBy(x => x.Id),

            "publicationyear" => descending
                ? query.OrderByDescending(x => x.GodinaIzdanja).ThenBy(x => x.Id)
                : query.OrderBy(x => x.GodinaIzdanja).ThenBy(x => x.Id),

            "pagecount" => descending
                ? query.OrderByDescending(x => x.BrojStranica).ThenBy(x => x.Id)
                : query.OrderBy(x => x.BrojStranica).ThenBy(x => x.Id),

            _ => descending
                ? query.OrderByDescending(x => x.Naslov).ThenBy(x => x.Id)
                : query.OrderBy(x => x.Naslov).ThenBy(x => x.Id)
        };

        var projectedQuery = query
            .Select(x => new GetPagedBooksItemDto
            {
                Id = x.Id,
                Title = x.Naslov,
                Isbn = x.Isbn,
                PublicationYear = x.GodinaIzdanja,
                PageCount = x.BrojStranica,
                LanguageId = x.JezikId,
                PublisherId = x.PublisherId,
                AvailableCopies = x.DostupnoPrimjeraka,
                AverageRating = x.ProsjecnaOcjena
            });

        return await PageResult<GetPagedBooksItemDto>
            .FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}
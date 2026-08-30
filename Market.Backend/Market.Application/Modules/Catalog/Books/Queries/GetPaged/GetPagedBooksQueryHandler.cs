using Market.Application.Modules.Catalog.Books.Queries.GetPaged;

public sealed class GetPagedBooksQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetPagedBooksQuery, PageResult<GetPagedBooksItemDto>>
{
    public async Task<PageResult<GetPagedBooksItemDto>> Handle(
        GetPagedBooksQuery request,
        CancellationToken ct)
    {
        var query = ctx.Books
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            var title = request.Title.Trim();

            query = query.Where(x => x.Title.Contains(title));
        }

        if (!string.IsNullOrWhiteSpace(request.Isbn))
        {
            var isbn = request.Isbn.Trim();

            query = query.Where(x => x.Isbn.Contains(isbn));
        }

        if (request.AuthorId.HasValue)
        {
            query = query.Where(x =>
                x.Authors.Any(a => a.AuthorId == request.AuthorId.Value));
        }

        if (request.GenreId.HasValue)
        {
            query = query.Where(x =>
                x.Genres.Any(g => g.GenreId == request.GenreId.Value));
        }

        if (request.LanguageId.HasValue)
        {
            query = query.Where(x =>
                x.LanguageId == request.LanguageId.Value);
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
                ? query.OrderByDescending(x => x.PublicationYear).ThenBy(x => x.Id)
                : query.OrderBy(x => x.PublicationYear).ThenBy(x => x.Id),

            "pagecount" => descending
                ? query.OrderByDescending(x => x.PageCount).ThenBy(x => x.Id)
                : query.OrderBy(x => x.PageCount).ThenBy(x => x.Id),

            _ => descending
                ? query.OrderByDescending(x => x.Title).ThenBy(x => x.Id)
                : query.OrderBy(x => x.Title).ThenBy(x => x.Id)
        };

        var projectedQuery = query
            .Select(x => new GetPagedBooksItemDto
            {
                Id = x.Id,
                Title = x.Title,
                Isbn = x.Isbn,
                PublicationYear = x.PublicationYear,
                PageCount = x.PageCount,
                LanguageId = x.LanguageId,
                PublisherId = x.PublisherId,
                AvailableCopies = x.AvailableCopies,
                AverageRating = x.AverageRating
            });

        return await PageResult<GetPagedBooksItemDto>
            .FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}
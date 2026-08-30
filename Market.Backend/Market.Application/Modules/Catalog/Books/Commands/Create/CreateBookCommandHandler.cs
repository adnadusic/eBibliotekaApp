using Market.Application.Modules.Catalog.Books.Commands.Create;
using Market.Domain.Entities.Catalog;
using Market.Domain.Enums;

public sealed class CreateBookCommandHandler(
    IAppDbContext ctx)
    : IRequestHandler<CreateBookCommand, CreateBookCommandDto>
{
    public async Task<CreateBookCommandDto> Handle(
        CreateBookCommand request,
        CancellationToken ct)
    {
        var isbn = request.Isbn.Trim();

        // ISBN remains reserved even after a book is soft deleted, matching
        // the database-wide unique ISBN constraint. Ignore the global
        // soft-delete filter so deleted books are included in the check.
        var isbnExists = await ctx.Books
            .IgnoreQueryFilters()
            .AnyAsync(x => x.Isbn == isbn, ct);

        if (isbnExists)
        {
            throw new MarketConflictException(
                "A book with the same ISBN already exists.");
        }

        var languageExists = await ctx.Languages
            .AnyAsync(x => x.Id == request.LanguageId && !x.IsDeleted, ct);

        if (!languageExists)
        {
            throw new MarketNotFoundException("Language was not found.");
        }

        if (request.PublisherId.HasValue)
        {
            var publisherExists = await ctx.Publishers
                .AnyAsync(
                    x => x.Id == request.PublisherId.Value && !x.IsDeleted,
                    ct);

            if (!publisherExists)
            {
                throw new MarketNotFoundException("Publisher was not found.");
            }
        }

        var authorIds = request.AuthorIds
            .Distinct()
            .ToList();

        if (authorIds.Count > 0)
        {
            var existingAuthorIds = await ctx.Authors
                .Where(x => authorIds.Contains(x.Id) && !x.IsDeleted)
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (existingAuthorIds.Count != authorIds.Count)
            {
                throw new MarketNotFoundException(
                    "One or more authors were not found.");
            }
        }

        var genreIds = request.GenreIds
            .Distinct()
            .ToList();

        if (genreIds.Count > 0)
        {
            var existingGenreIds = await ctx.Genres
                .Where(x => genreIds.Contains(x.Id) && !x.IsDeleted)
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (existingGenreIds.Count != genreIds.Count)
            {
                throw new MarketNotFoundException(
                    "One or more genres were not found.");
            }
        }

        var book = new Book
        {
            Title = request.Title.Trim(),
            Isbn = isbn,
            PublicationYear = request.PublicationYear,
            PageCount = request.PageCount,
            LanguageId = request.LanguageId,
            PublisherId = request.PublisherId,
            Description = request.Description?.Trim() ?? string.Empty,
            CoverImage = request.CoverImage?.Trim() ?? string.Empty,
            TotalCopies = 0,
            AvailableCopies = 0,
            AverageRating = 0,
            RatingCount = 0,
            ViewCount = 0,
            AddedAt = DateTime.UtcNow,

            Authors = authorIds
                .Select(authorId => new BookAuthor
                {
                    AuthorId = authorId,
                    ContributionType = ContributionType.Author
                })
                .ToList(),

            Genres = genreIds
                .Select(genreId => new BookGenre
                {
                    GenreId = genreId
                })
                .ToList()
        };

        ctx.Books.Add(book);

        await ctx.SaveChangesAsync(ct);

        return new CreateBookCommandDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn
        };
    }
}
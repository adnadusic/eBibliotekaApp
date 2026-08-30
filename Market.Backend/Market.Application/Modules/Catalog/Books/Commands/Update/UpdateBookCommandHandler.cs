using Market.Application.Modules.Catalog.Books.Commands.Update;
using Market.Domain.Entities.Catalog;
using Market.Domain.Enums;

public sealed class UpdateBookCommandHandler(
    IAppDbContext ctx)
    : IRequestHandler<UpdateBookCommand, UpdateBookCommandDto>
{
    public async Task<UpdateBookCommandDto> Handle(
        UpdateBookCommand request,
        CancellationToken ct)
    {
        var book = await ctx.Books
            .Include(x => x.Authors)
            .Include(x => x.Genres)
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                ct)
            ?? throw new MarketNotFoundException(
                "Book was not found.");

        var isbn = request.Isbn.Trim();

        // Keep ISBN reserved across soft deletes and exclude the book that is
        // currently being updated from the duplicate check.
        var isbnExists = await ctx.Books
            .IgnoreQueryFilters()
            .AnyAsync(
                x => x.Id != book.Id && x.Isbn == isbn,
                ct);

        if (isbnExists)
        {
            throw new MarketConflictException(
                "A book with the same ISBN already exists.");
        }

        var languageExists = await ctx.Languages
            .AnyAsync(
                x => x.Id == request.LanguageId && !x.IsDeleted,
                ct);

        if (!languageExists)
        {
            throw new MarketNotFoundException(
                "Language was not found.");
        }

        if (request.PublisherId.HasValue)
        {
            var publisherExists = await ctx.Publishers
                .AnyAsync(
                    x =>
                        x.Id == request.PublisherId.Value &&
                        !x.IsDeleted,
                    ct);

            if (!publisherExists)
            {
                throw new MarketNotFoundException(
                    "Publisher was not found.");
            }
        }

        var authorIds = request.AuthorIds
            .Distinct()
            .ToList();

        if (authorIds.Count > 0)
        {
            var existingAuthorIds = await ctx.Authors
                .Where(
                    x =>
                        authorIds.Contains(x.Id) &&
                        !x.IsDeleted)
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
                .Where(
                    x =>
                        genreIds.Contains(x.Id) &&
                        !x.IsDeleted)
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (existingGenreIds.Count != genreIds.Count)
            {
                throw new MarketNotFoundException(
                    "One or more genres were not found.");
            }
        }

        book.Title = request.Title.Trim();
        book.Isbn = isbn;
        book.PublicationYear = request.PublicationYear;
        book.PageCount = request.PageCount;
        book.LanguageId = request.LanguageId;
        book.PublisherId = request.PublisherId;
        book.Description =
            request.Description?.Trim() ?? string.Empty;
        book.CoverImage =
            request.CoverImage?.Trim() ?? string.Empty;

        book.Authors.Clear();

        foreach (var authorId in authorIds)
        {
            book.Authors.Add(new BookAuthor
            {
                AuthorId = authorId,
                ContributionType = ContributionType.Author
            });
        }

        book.Genres.Clear();

        foreach (var genreId in genreIds)
        {
            book.Genres.Add(new BookGenre
            {
                GenreId = genreId
            });
        }

        await ctx.SaveChangesAsync(ct);

        return new UpdateBookCommandDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn
        };
    }
}
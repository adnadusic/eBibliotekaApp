using Market.Application.Modules.Catalog.Books.Commands.Create;
using Market.Domain.Entities.Catalog;
using Market.Domain.Enums;

public sealed class CreateBookCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateBookCommand, CreateBookCommandDto>
{
    public async Task<CreateBookCommandDto> Handle(
        CreateBookCommand request,
        CancellationToken ct)
    {
        var isbn = request.Isbn.Trim();

        var isbnExists = await ctx.Knjige
            .AnyAsync(x => x.Isbn == isbn && !x.IsDeleted, ct);

        if (isbnExists)
            throw new MarketConflictException(
                "A book with the same ISBN already exists.");

        var languageExists = await ctx.Jezici
            .AnyAsync(x => x.Id == request.LanguageId && !x.IsDeleted, ct);

        if (!languageExists)
            throw new MarketNotFoundException("Language was not found.");

        if (request.PublisherId.HasValue)
        {
            var publisherExists = await ctx.Izdavaci
                .AnyAsync(
                    x => x.Id == request.PublisherId.Value && !x.IsDeleted,
                    ct);

            if (!publisherExists)
                throw new MarketNotFoundException("Publisher was not found.");
        }

        var authorIds = request.AuthorIds
            .Distinct()
            .ToList();

        if (authorIds.Count > 0)
        {
            var existingAuthorIds = await ctx.Autori
                .Where(x => authorIds.Contains(x.Id) && !x.IsDeleted)
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (existingAuthorIds.Count != authorIds.Count)
                throw new MarketNotFoundException(
                    "One or more authors were not found.");
        }

        var genreIds = request.GenreIds
            .Distinct()
            .ToList();

        if (genreIds.Count > 0)
        {
            var existingGenreIds = await ctx.Zanrovi
                .Where(x => genreIds.Contains(x.Id) && !x.IsDeleted)
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (existingGenreIds.Count != genreIds.Count)
                throw new MarketNotFoundException(
                    "One or more genres were not found.");
        }

        var book = new Knjiga
        {
            Naslov = request.Title.Trim(),
            Isbn = isbn,
            GodinaIzdanja = request.PublicationYear,
            BrojStranica = request.PageCount,
            JezikId = request.LanguageId,
            PublisherId = request.PublisherId,
            Opis = request.Description?.Trim() ?? string.Empty,
            SlikaKorice = request.CoverImage?.Trim() ?? string.Empty,
            UkupnoPrimjeraka = 0,
            DostupnoPrimjeraka = 0,
            ProsjecnaOcjena = 0,
            BrojOcjena = 0,
            BrojPregleda = 0,
            DatumDodavanja = DateTime.UtcNow,
            Autori = authorIds
                .Select(authorId => new KnjigaAutor
                {
                    AuthorId = authorId,
                    TipDoprinosa = ContributionType.Author
                })
                .ToList(),
            Zanrovi = genreIds
                .Select(genreId => new KnjigaZanr
                {
                    GenreId = genreId
                })
                .ToList()
        };

        ctx.Knjige.Add(book);

        await ctx.SaveChangesAsync(ct);

        return new CreateBookCommandDto
        {
            Id = book.Id,
            Title = book.Naslov,
            Isbn = book.Isbn
        };
    }
}
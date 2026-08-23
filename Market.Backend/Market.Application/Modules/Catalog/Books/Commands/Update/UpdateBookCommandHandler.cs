using Market.Application.Modules.Catalog.Books.Commands.Update;
using Market.Domain.Entities.Catalog;
using Market.Domain.Enums;

public sealed class UpdateBookCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateBookCommand, UpdateBookCommandDto>
{
    public async Task<UpdateBookCommandDto> Handle(
        UpdateBookCommand request,
        CancellationToken ct)
    {
        var book = await ctx.Knjige
            .Include(x => x.Autori)
            .Include(x => x.Zanrovi)
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, ct)
            ?? throw new MarketNotFoundException("Book was not found.");

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
                throw new MarketNotFoundException("One or more authors were not found.");
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
                throw new MarketNotFoundException("One or more genres were not found.");
        }

        book.Naslov = request.Title.Trim();
        book.Isbn = request.Isbn.Trim();
        book.GodinaIzdanja = request.PublicationYear;
        book.BrojStranica = request.PageCount;
        book.JezikId = request.LanguageId;
        book.PublisherId = request.PublisherId;
        book.Opis = request.Description?.Trim();
        book.SlikaKorice = request.CoverImage?.Trim();

        book.Autori.Clear();

        foreach (var authorId in authorIds)
        {
            book.Autori.Add(new KnjigaAutor
            {
                AuthorId = authorId,
                TipDoprinosa = ContributionType.Author
            });
        }

        book.Zanrovi.Clear();

        foreach (var genreId in genreIds)
        {
            book.Zanrovi.Add(new KnjigaZanr
            {
                GenreId = genreId
            });
        }

        await ctx.SaveChangesAsync(ct);

        return new UpdateBookCommandDto
        {
            Id = book.Id,
            Title = book.Naslov,
            Isbn = book.Isbn
        };
    }
}
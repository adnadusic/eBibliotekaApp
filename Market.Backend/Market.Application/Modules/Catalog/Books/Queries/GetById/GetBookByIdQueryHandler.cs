using Market.Application.Modules.Catalog.Books.Queries.GetById;

public sealed class GetBookByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetBookByIdQuery, GetBookByIdDto>
{
    public async Task<GetBookByIdDto> Handle(
        GetBookByIdQuery request,
        CancellationToken ct)
    {
        var book = await ctx.Knjige
            .AsNoTracking()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new GetBookByIdDto
            {
                Id = x.Id,
                Title = x.Naslov,
                Isbn = x.Isbn,
                PublicationYear = x.GodinaIzdanja,
                PageCount = x.BrojStranica,
                LanguageId = x.JezikId,
                PublisherId = x.PublisherId,
                Description = x.Opis,
                CoverImage = x.SlikaKorice,
                TotalCopies = x.UkupnoPrimjeraka,
                AvailableCopies = x.DostupnoPrimjeraka,
                AverageRating = x.ProsjecnaOcjena,
                RatingCount = x.BrojOcjena,
                ViewCount = x.BrojPregleda,
                AddedAt = x.DatumDodavanja,
                AuthorIds = x.Autori
                    .Select(a => a.AuthorId)
                    .ToList(),
                GenreIds = x.Zanrovi
                    .Select(g => g.GenreId)
                    .ToList()
            })
            .FirstOrDefaultAsync(ct)
            ?? throw new MarketNotFoundException("Book was not found.");

        return book;
    }
}
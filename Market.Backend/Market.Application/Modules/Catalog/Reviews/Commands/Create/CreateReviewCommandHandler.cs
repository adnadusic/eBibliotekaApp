using Market.Domain.Entities.Catalog;

namespace Market.Application.Modules.Catalog.Reviews.Commands.Create;

public sealed class CreateReviewCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<CreateReviewCommand, CreateReviewCommandDto>
{
    public async Task<CreateReviewCommandDto> Handle(
        CreateReviewCommand request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var book = await ctx.Knjige
            .FirstOrDefaultAsync(
                x => x.Id == request.BookId && !x.IsDeleted,
                ct);

        if (book is null)
        {
            throw new MarketNotFoundException("Book was not found.");
        }

        var reviewExists = await ctx.Recenzije
            .AnyAsync(
                x =>
                    x.BookId == request.BookId &&
                    x.UserId == userId &&
                    !x.IsDeleted,
                ct);

        if (reviewExists)
        {
            throw new MarketConflictException(
                "You have already reviewed this book.");
        }

        var review = new Recenzija
        {
            UserId = userId,
            BookId = request.BookId,
            Ocjena = request.Rating,
            Naslov = request.Title.Trim(),
            Komentar = request.Comment.Trim(),
            Datum = DateTime.UtcNow,
            BrojHelpful = 0,
            BrojUnhelpful = 0,
            Izmijenjeno = false,
            DatumIzmjene = null
        };

        ctx.Recenzije.Add(review);

        var currentRatingCount = book.BrojOcjena;
        var currentAverageRating = book.ProsjecnaOcjena;

        var newRatingCount = currentRatingCount + 1;

        book.ProsjecnaOcjena =
            ((currentAverageRating * currentRatingCount) + request.Rating)
            / newRatingCount;

        book.BrojOcjena = newRatingCount;

        await ctx.SaveChangesAsync(ct);

        return new CreateReviewCommandDto
        {
            Id = review.Id,
            BookId = review.BookId,
            UserId = review.UserId,
            Rating = review.Ocjena,
            Title = review.Naslov,
            Comment = review.Komentar,
            Date = review.Datum
        };
    }
}
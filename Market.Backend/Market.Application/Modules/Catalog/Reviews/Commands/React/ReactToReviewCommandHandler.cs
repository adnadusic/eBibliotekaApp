using Market.Domain.Entities.Catalog;
using Market.Domain.Enums;

namespace Market.Application.Modules.Catalog.Reviews.Commands.React;

public sealed class ReactToReviewCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<ReactToReviewCommand>
{
    public async Task Handle(
        ReactToReviewCommand request,
        CancellationToken ct)
    {
        if (!currentUser.UserId.HasValue)
        {
            throw new InvalidOperationException(
                "Authenticated user identifier is missing.");
        }

        var userId = currentUser.UserId.Value;

        var review = await ctx.Recenzije
            .FirstOrDefaultAsync(
                x => x.Id == request.ReviewId && !x.IsDeleted,
                ct);

        if (review is null)
        {
            throw new MarketNotFoundException("Review was not found.");
        }

        var existingReaction = await ctx.OcjeneRecenzija
            .FirstOrDefaultAsync(
                x =>
                    x.ReviewId == request.ReviewId &&
                    x.UserId == userId &&
                    !x.IsDeleted,
                ct);

        if (existingReaction is null)
        {
            var reaction = new OcjenaRecenzije
            {
                UserId = userId,
                ReviewId = request.ReviewId,
                TipOcjene = request.ReactionType,
                Datum = DateTime.UtcNow
            };

            ctx.OcjeneRecenzija.Add(reaction);

            if (request.ReactionType == ReviewRatingType.Helpful)
            {
                review.BrojHelpful++;
            }
            else
            {
                review.BrojUnhelpful++;
            }
        }
        else if (existingReaction.TipOcjene != request.ReactionType)
        {
            if (existingReaction.TipOcjene == ReviewRatingType.Helpful)
            {
                review.BrojHelpful = Math.Max(
                    0,
                    review.BrojHelpful - 1);

                review.BrojUnhelpful++;
            }
            else
            {
                review.BrojUnhelpful = Math.Max(
                    0,
                    review.BrojUnhelpful - 1);

                review.BrojHelpful++;
            }

            existingReaction.TipOcjene = request.ReactionType;
            existingReaction.Datum = DateTime.UtcNow;
        }

        await ctx.SaveChangesAsync(ct);
    }
}
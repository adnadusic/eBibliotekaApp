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

        var review = await ctx.Reviews
            .FirstOrDefaultAsync(
                x => x.Id == request.ReviewId && !x.IsDeleted,
                ct);

        if (review is null)
        {
            throw new MarketNotFoundException("Review was not found.");
        }

        var existingReaction = await ctx.ReviewReactions
            .FirstOrDefaultAsync(
                x =>
                    x.ReviewId == request.ReviewId &&
                    x.UserId == userId &&
                    !x.IsDeleted,
                ct);

        if (existingReaction is null)
        {
            var reaction = new ReviewReaction
            {
                UserId = userId,
                ReviewId = request.ReviewId,
                ReactionType = request.ReactionType,
                CreatedAt = DateTime.UtcNow
            };

            ctx.ReviewReactions.Add(reaction);

            switch (request.ReactionType)
            {
                case ReviewRatingType.Helpful:
                    review.HelpfulCount++;
                    break;
                case ReviewRatingType.Unhelpful:
                    review.UnhelpfulCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(request.ReactionType),
                        request.ReactionType,
                        "Unsupported review reaction type.");
            }
        }
        else if (existingReaction.ReactionType != request.ReactionType)
        {
            switch (existingReaction.ReactionType, request.ReactionType)
            {
                case (ReviewRatingType.Helpful, ReviewRatingType.Unhelpful):
                    review.HelpfulCount = Math.Max(
                        0,
                        review.HelpfulCount - 1);

                    review.UnhelpfulCount++;
                    break;

                case (ReviewRatingType.Unhelpful, ReviewRatingType.Helpful):
                    review.UnhelpfulCount = Math.Max(
                        0,
                        review.UnhelpfulCount - 1);

                    review.HelpfulCount++;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(request.ReactionType),
                        request.ReactionType,
                        "Unsupported review reaction transition.");
            }

            existingReaction.ReactionType = request.ReactionType;
            existingReaction.CreatedAt = DateTime.UtcNow;
        }

        await ctx.SaveChangesAsync(ct);
    }
}
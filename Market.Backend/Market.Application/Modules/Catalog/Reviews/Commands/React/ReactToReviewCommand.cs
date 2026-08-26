using Market.Domain.Enums;

namespace Market.Application.Modules.Catalog.Reviews.Commands.React;

public sealed class ReactToReviewCommand : IRequest
{
    public int ReviewId { get; init; }

    public ReviewRatingType ReactionType { get; init; }
}
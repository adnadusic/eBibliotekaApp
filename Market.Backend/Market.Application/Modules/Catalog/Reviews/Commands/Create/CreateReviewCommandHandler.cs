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

        var book = await ctx.Books
            .FirstOrDefaultAsync(
                x => x.Id == request.BookId && !x.IsDeleted,
                ct);

        if (book is null)
        {
            throw new MarketNotFoundException("Book was not found.");
        }

        var reviewExists = await ctx.Reviews
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

        var review = new Review
        {
            UserId = userId,
            BookId = request.BookId,
            Rating = request.Rating,
            Title = request.Title.Trim(),
            Comment = request.Comment.Trim(),
            CreatedAt = DateTime.UtcNow,
            HelpfulCount = 0,
            UnhelpfulCount = 0,
            IsEdited = false,
            EditedAt = null
        };

        ctx.Reviews.Add(review);

        var currentRatingCount = book.RatingCount;
        var currentAverageRating = book.AverageRating;

        var newRatingCount = currentRatingCount + 1;

        book.AverageRating =
            ((currentAverageRating * currentRatingCount) + request.Rating)
            / newRatingCount;

        book.RatingCount = newRatingCount;

        await ctx.SaveChangesAsync(ct);

        return new CreateReviewCommandDto
        {
            Id = review.Id,
            BookId = review.BookId,
            UserId = review.UserId,
            Rating = review.Rating,
            Title = review.Title,
            Comment = review.Comment,
            Date = review.CreatedAt
        };
    }
}
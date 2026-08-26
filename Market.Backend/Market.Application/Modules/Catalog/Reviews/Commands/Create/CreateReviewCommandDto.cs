namespace Market.Application.Modules.Catalog.Reviews.Commands.Create;

/// <summary>
/// Result returned after creating a review.
/// </summary>
public sealed class CreateReviewCommandDto
{
    public int Id { get; init; }

    public int BookId { get; init; }

    public int UserId { get; init; }

    public int Rating { get; init; }

    public string Title { get; init; }

    public string Comment { get; init; }

    public DateTime Date { get; init; }
}
namespace Market.Application.Modules.Catalog.Reviews.Commands.Create;

/// <summary>
/// Command for creating a review and rating for a book.
/// </summary>
public sealed class CreateReviewCommand : IRequest<CreateReviewCommandDto>
{
    /// <summary>
    /// Book identifier.
    /// </summary>
    public int BookId { get; init; }

    /// <summary>
    /// Rating from 1 to 5.
    /// </summary>
    public int Rating { get; init; }

    /// <summary>
    /// Review title.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Review comment.
    /// </summary>
    public string Comment { get; init; }
}
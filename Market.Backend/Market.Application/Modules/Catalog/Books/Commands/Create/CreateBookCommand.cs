namespace Market.Application.Modules.Catalog.Books.Commands.Create;

/// <summary>
/// Command for creating a new book.
/// </summary>
public sealed class CreateBookCommand : IRequest<CreateBookCommandDto>
{
    /// <summary>
    /// Book title.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// ISBN number.
    /// </summary>
    public string Isbn { get; init; }

    /// <summary>
    /// Publication year.
    /// </summary>
    public int? PublicationYear { get; init; }

    /// <summary>
    /// Number of pages.
    /// </summary>
    public int? PageCount { get; init; }

    /// <summary>
    /// Language identifier.
    /// </summary>
    public int LanguageId { get; init; }

    /// <summary>
    /// Publisher identifier.
    /// </summary>
    public int? PublisherId { get; init; }

    /// <summary>
    /// Book description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Cover image path or URL.
    /// </summary>
    public string? CoverImage { get; init; }

    /// <summary>
    /// Authors associated with the book.
    /// </summary>
    public List<int> AuthorIds { get; init; } = [];

    /// <summary>
    /// Genres associated with the book.
    /// </summary>
    public List<int> GenreIds { get; init; } = [];
}
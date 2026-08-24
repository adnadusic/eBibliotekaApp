namespace Market.Application.Modules.Catalog.Books.Queries.GetPaged;

/// <summary>
/// Query for retrieving a paged and filtered list of books.
/// </summary>
public sealed class GetPagedBooksQuery
    : BasePagedQuery<GetPagedBooksItemDto>
{
    /// <summary>
    /// Filters books by title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Filters books by ISBN.
    /// </summary>
    public string? Isbn { get; init; }

    /// <summary>
    /// Filters books by author.
    /// </summary>
    public int? AuthorId { get; init; }

    /// <summary>
    /// Filters books by genre.
    /// </summary>
    public int? GenreId { get; init; }

    /// <summary>
    /// Filters books by language.
    /// </summary>
    public int? LanguageId { get; init; }
}
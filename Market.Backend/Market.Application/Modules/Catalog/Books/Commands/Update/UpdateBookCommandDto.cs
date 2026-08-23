namespace Market.Application.Modules.Catalog.Books.Commands.Update;

/// <summary>
/// Represents the result of updating an existing book.
/// </summary>
public sealed class UpdateBookCommandDto
{
    /// <summary>
    /// Identifier of the updated book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Updated book title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Updated ISBN.
    /// </summary>
    public string Isbn { get; set; }
}
namespace Market.Application.Modules.Catalog.Books.Commands.Create;

/// <summary>
/// Represents the result of creating a new book.
/// </summary>
public sealed class CreateBookCommandDto
{
    /// <summary>
    /// Identifier of the newly created book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the newly created book.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ISBN of the newly created book.
    /// </summary>
    public string Isbn { get; set; }
}
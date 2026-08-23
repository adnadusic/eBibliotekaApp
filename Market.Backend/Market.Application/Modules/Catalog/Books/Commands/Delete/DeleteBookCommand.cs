namespace Market.Application.Modules.Catalog.Books.Commands.Delete;

/// <summary>
/// Command for deleting an existing book.
/// </summary>
public sealed class DeleteBookCommand : IRequest
{
    /// <summary>
    /// Book identifier.
    /// </summary>
    public int Id { get; init; }
}
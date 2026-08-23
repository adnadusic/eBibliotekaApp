namespace Market.Application.Modules.Catalog.Books.Queries.GetById;

/// <summary>
/// Query for retrieving a single book by identifier.
/// </summary>
public sealed class GetBookByIdQuery : IRequest<GetBookByIdDto>
{
    /// <summary>
    /// Book identifier.
    /// </summary>
    public int Id { get; init; }
}
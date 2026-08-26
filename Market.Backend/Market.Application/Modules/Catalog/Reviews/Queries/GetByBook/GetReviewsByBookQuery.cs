namespace Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

public sealed class GetReviewsByBookQuery
    : IRequest<List<GetReviewsByBookItemDto>>
{
    public int BookId { get; init; }
}
namespace Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

public sealed class GetReviewsByBookQuery
    : BasePagedQuery<GetReviewsByBookItemDto>
{
    public int BookId { get; init; }
}
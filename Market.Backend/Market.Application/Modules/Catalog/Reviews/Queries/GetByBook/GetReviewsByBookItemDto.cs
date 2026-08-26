namespace Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

public sealed class GetReviewsByBookItemDto
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string UserName { get; init; }

    public int Rating { get; init; }

    public string Title { get; init; }

    public string Comment { get; init; }

    public DateTime Date { get; init; }

    public int HelpfulCount { get; init; }

    public int UnhelpfulCount { get; init; }
}
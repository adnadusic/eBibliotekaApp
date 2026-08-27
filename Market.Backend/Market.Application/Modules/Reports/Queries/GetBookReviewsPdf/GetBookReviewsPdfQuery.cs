namespace Market.Application.Modules.Reports.Queries.GetBookReviewsPdf;

public sealed class GetBookReviewsPdfQuery : IRequest<byte[]>
{
    public int BookId { get; init; }

    public DateTime DateFrom { get; init; }

    public DateTime DateTo { get; init; }
}
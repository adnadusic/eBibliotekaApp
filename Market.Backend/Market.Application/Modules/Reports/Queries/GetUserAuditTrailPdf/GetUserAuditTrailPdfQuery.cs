namespace Market.Application.Modules.Reports.Queries.GetUserAuditTrailPdf;

public sealed class GetUserAuditTrailPdfQuery : IRequest<byte[]>
{
    public int UserId { get; init; }

    public DateTime DateFrom { get; init; }

    public DateTime DateTo { get; init; }
}
using Market.Application.Modules.Reports.Queries.GetBookReviewsPdf;
using Market.Application.Modules.Reports.Queries.GetUserAuditTrailPdf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public sealed class ReportsController(IMediator mediator)
    : ControllerBase
{
    [HttpGet("book-reviews")]
    public async Task<IActionResult> GetBookReviewsPdf(
        [FromQuery] int bookId,
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo,
        CancellationToken ct)
    {
        var pdf = await mediator.Send(
            new GetBookReviewsPdfQuery
            {
                BookId = bookId,
                DateFrom = dateFrom,
                DateTo = dateTo
            },
            ct);

        var fileName =
            $"book-reviews-{bookId}-{dateFrom:yyyyMMdd}-{dateTo:yyyyMMdd}.pdf";

        return File(
            pdf,
            "application/pdf",
            fileName);
    }

    [HttpGet("audit-trail")]
    public async Task<IActionResult> GetAuditTrailPdf(
        [FromQuery] int userId,
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo,
        CancellationToken ct)
    {
        var pdf = await mediator.Send(
            new GetUserAuditTrailPdfQuery
            {
                UserId = userId,
                DateFrom = dateFrom,
                DateTo = dateTo
            },
            ct);

        var fileName =
            $"audit-trail-{userId}-{dateFrom:yyyyMMdd}-{dateTo:yyyyMMdd}.pdf";

        return File(
            pdf,
            "application/pdf",
            fileName);
    }
}
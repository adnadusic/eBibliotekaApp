using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Market.Application.Modules.Reports.Queries.GetBookReviewsPdf;

public sealed class GetBookReviewsPdfQueryHandler(
    IAppDbContext ctx)
    : IRequestHandler<GetBookReviewsPdfQuery, byte[]>
{
    public async Task<byte[]> Handle(
        GetBookReviewsPdfQuery request,
        CancellationToken ct)
    {
        var book = await ctx.Books
            .AsNoTracking()
            .Where(x => x.Id == request.BookId)
            .Select(x => new
            {
                x.Id,
                x.Title
            })
            .FirstOrDefaultAsync(ct);

        if (book is null)
        {
            throw new InvalidOperationException(
                "Book was not found.");
        }

        var dateFrom = request.DateFrom.Date;
        var dateToExclusive = request.DateTo.Date.AddDays(1);

        var reviews = await ctx.Reviews
            .AsNoTracking()
            .Where(x =>
                x.BookId == request.BookId &&
                x.CreatedAt >= dateFrom &&
                x.CreatedAt < dateToExclusive)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.Id,
                x.UserId,
                x.Rating,
                x.Title,
                x.Comment,
                x.CreatedAt
            })
            .ToListAsync(ct);

        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                page.DefaultTextStyle(x =>
                    x.FontSize(10));

                page.Header()
                    .Column(column =>
                    {
                        column.Item()
                            .Text("eBiblioteka - Review Report")
                            .SemiBold()
                            .FontSize(18);

                        column.Item()
                            .Text($"Book: {book.Title}");

                        column.Item()
                            .Text(
                                $"Period: {dateFrom:dd.MM.yyyy} - {request.DateTo.Date:dd.MM.yyyy}");

                        column.Item()
                            .Text(
                                $"Number of reviews: {reviews.Count}");
                    });

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        if (reviews.Count == 0)
                        {
                            column.Item()
                                .Text(
                                    "There are no reviews for the selected period.");

                            return;
                        }

                        column.Item()
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(35);
                                    columns.ConstantColumn(55);
                                    columns.ConstantColumn(55);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                    columns.ConstantColumn(70);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("ID").SemiBold();
                                    header.Cell().Text("User").SemiBold();
                                    header.Cell().Text("Rating").SemiBold();
                                    header.Cell().Text("Title").SemiBold();
                                    header.Cell().Text("Comment").SemiBold();
                                    header.Cell().Text("Date").SemiBold();
                                });

                                foreach (var review in reviews)
                                {
                                    table.Cell()
                                        .Text(review.Id.ToString());

                                    table.Cell()
                                        .Text(review.UserId.ToString());

                                    table.Cell()
                                        .Text(review.Rating.ToString());

                                    table.Cell()
                                        .Text(
                                            string.IsNullOrWhiteSpace(review.Title)
                                                ? "-"
                                                : review.Title);

                                    table.Cell()
                                        .Text(
                                            string.IsNullOrWhiteSpace(review.Comment)
                                                ? "-"
                                                : review.Comment);

                                    table.Cell()
                                        .Text(
                                            review.CreatedAt.ToString(
                                                "dd.MM.yyyy"));
                                }
                            });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
            });
        });

        return document.GeneratePdf();
    }
}
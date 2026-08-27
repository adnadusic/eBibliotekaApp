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
        var book = await ctx.Knjige
            .AsNoTracking()
            .Where(x => x.Id == request.BookId)
            .Select(x => new
            {
                x.Id,
                x.Naslov
            })
            .FirstOrDefaultAsync(ct);

        if (book is null)
        {
            throw new InvalidOperationException(
                "Book was not found.");
        }

        var dateFrom = request.DateFrom.Date;
        var dateToExclusive = request.DateTo.Date.AddDays(1);

        var reviews = await ctx.Recenzije
            .AsNoTracking()
            .Where(x =>
                x.BookId == request.BookId &&
                x.Datum >= dateFrom &&
                x.Datum < dateToExclusive)
            .OrderByDescending(x => x.Datum)
            .Select(x => new
            {
                x.Id,
                x.UserId,
                x.Ocjena,
                x.Naslov,
                x.Komentar,
                x.Datum
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
                            .Text("eBiblioteka - Izvještaj recenzija")
                            .SemiBold()
                            .FontSize(18);

                        column.Item()
                            .Text($"Knjiga: {book.Naslov}");

                        column.Item()
                            .Text(
                                $"Period: {dateFrom:dd.MM.yyyy} - {request.DateTo.Date:dd.MM.yyyy}");

                        column.Item()
                            .Text(
                                $"Broj recenzija: {reviews.Count}");
                    });

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        if (reviews.Count == 0)
                        {
                            column.Item()
                                .Text(
                                    "Nema recenzija za odabrani period.");

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
                                    header.Cell().Text("Ocjena").SemiBold();
                                    header.Cell().Text("Naslov").SemiBold();
                                    header.Cell().Text("Komentar").SemiBold();
                                    header.Cell().Text("Datum").SemiBold();
                                });

                                foreach (var review in reviews)
                                {
                                    table.Cell()
                                        .Text(review.Id.ToString());

                                    table.Cell()
                                        .Text(review.UserId.ToString());

                                    table.Cell()
                                        .Text(review.Ocjena.ToString());

                                    table.Cell()
                                        .Text(
                                            string.IsNullOrWhiteSpace(review.Naslov)
                                                ? "-"
                                                : review.Naslov);

                                    table.Cell()
                                        .Text(
                                            string.IsNullOrWhiteSpace(review.Komentar)
                                                ? "-"
                                                : review.Komentar);

                                    table.Cell()
                                        .Text(
                                            review.Datum.ToString(
                                                "dd.MM.yyyy"));
                                }
                            });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Stranica ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
            });
        });

        return document.GeneratePdf();
    }
}
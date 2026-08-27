using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Market.Application.Modules.Reports.Queries.GetUserAuditTrailPdf;

public sealed class GetUserAuditTrailPdfQueryHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<GetUserAuditTrailPdfQuery, byte[]>
{
    public async Task<byte[]> Handle(
        GetUserAuditTrailPdfQuery request,
        CancellationToken ct)
    {
        if (!currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only administrators can generate Audit Trail reports.");
        }

        var dateFrom = request.DateFrom.Date;
        var dateToExclusive = request.DateTo.Date.AddDays(1);

        var auditLogs = await ctx.AuditLogs
            .AsNoTracking()
            .Where(x =>
                x.UserId == request.UserId &&
                x.ChangedAtUtc >= dateFrom &&
                x.ChangedAtUtc < dateToExclusive)
            .OrderByDescending(x => x.ChangedAtUtc)
            .Select(x => new
            {
                x.Id,
                x.UserId,
                x.UserEmail,
                x.EntityName,
                x.EntityId,
                x.Action,
                x.OldValues,
                x.NewValues,
                x.ChangedAtUtc
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
                            .Text("eBiblioteka - Audit Trail izvještaj")
                            .SemiBold()
                            .FontSize(18);

                        column.Item()
                            .Text(
                                $"Korisnik ID: {request.UserId}");

                        column.Item()
                            .Text(
                                $"Period: {dateFrom:dd.MM.yyyy} - {request.DateTo.Date:dd.MM.yyyy}");

                        column.Item()
                            .Text(
                                $"Broj zapisa: {auditLogs.Count}");
                    });

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Spacing(12);

                        if (auditLogs.Count == 0)
                        {
                            column.Item()
                                .Text(
                                    "Nema Audit Trail zapisa za odabranog korisnika i period.");

                            return;
                        }

                        foreach (var log in auditLogs)
                        {
                            column.Item()
                                .Border(1)
                                .Padding(10)
                                .Column(card =>
                                {
                                    card.Spacing(5);

                                    card.Item()
                                        .Text(
                                            $"{log.EntityName} - {log.Action}")
                                        .SemiBold()
                                        .FontSize(12);

                                    card.Item()
                                        .Text(
                                            $"Audit ID: {log.Id}");

                                    card.Item()
                                        .Text(
                                            $"Entity ID: {log.EntityId ?? "-"}");

                                    card.Item()
                                        .Text(
                                            $"Korisnik: {log.UserEmail ?? "Sistem"}");

                                    card.Item()
                                        .Text(
                                            $"Vrijeme: {log.ChangedAtUtc:dd.MM.yyyy HH:mm:ss} UTC");

                                    card.Item()
                                        .PaddingTop(5)
                                        .Text("Stare vrijednosti:")
                                        .SemiBold();

                                    card.Item()
                                        .Text(
                                            string.IsNullOrWhiteSpace(log.OldValues)
                                                ? "-"
                                                : log.OldValues);

                                    card.Item()
                                        .PaddingTop(5)
                                        .Text("Nove vrijednosti:")
                                        .SemiBold();

                                    card.Item()
                                        .Text(
                                            string.IsNullOrWhiteSpace(log.NewValues)
                                                ? "-"
                                                : log.NewValues);
                                });
                        }
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
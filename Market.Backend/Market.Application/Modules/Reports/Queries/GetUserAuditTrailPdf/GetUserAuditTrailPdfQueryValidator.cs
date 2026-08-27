namespace Market.Application.Modules.Reports.Queries.GetUserAuditTrailPdf;

public sealed class GetUserAuditTrailPdfQueryValidator
    : AbstractValidator<GetUserAuditTrailPdfQuery>
{
    public GetUserAuditTrailPdfQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.DateFrom)
            .NotEmpty();

        RuleFor(x => x.DateTo)
            .NotEmpty();

        RuleFor(x => x.DateTo)
            .GreaterThanOrEqualTo(x => x.DateFrom)
            .WithMessage(
                "DateTo must be greater than or equal to DateFrom.");
    }
}
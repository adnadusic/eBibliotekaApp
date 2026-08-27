namespace Market.Application.Modules.Reports.Queries.GetBookReviewsPdf;

public sealed class GetBookReviewsPdfQueryValidator
    : AbstractValidator<GetBookReviewsPdfQuery>
{
    public GetBookReviewsPdfQueryValidator()
    {
        RuleFor(x => x.BookId)
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
namespace Market.Application.Modules.Catalog.Books.Queries.GetPaged;

/// <summary>
/// FluentValidation validator for <see cref="GetPagedBooksQuery"/>.
/// </summary>
public sealed class GetPagedBooksQueryValidator : AbstractValidator<GetPagedBooksQuery>
{
    public GetPagedBooksQueryValidator()
    {
        RuleFor(x => x.Paging.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0.");

        RuleFor(x => x.Paging.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");

        RuleFor(x => x.Isbn)
            .MaximumLength(20)
            .WithMessage("ISBN filter can be up to 20 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Isbn));

        RuleFor(x => x.AuthorId)
            .GreaterThan(0)
            .WithMessage("Author identifier must be greater than 0.")
            .When(x => x.AuthorId.HasValue);

        RuleFor(x => x.GenreId)
            .GreaterThan(0)
            .WithMessage("Genre identifier must be greater than 0.")
            .When(x => x.GenreId.HasValue);

        RuleFor(x => x.LanguageId)
            .GreaterThan(0)
            .WithMessage("Language identifier must be greater than 0.")
            .When(x => x.LanguageId.HasValue);
    }
}
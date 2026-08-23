namespace Market.Application.Modules.Catalog.Books.Queries.GetById;

/// <summary>
/// FluentValidation validator for <see cref="GetBookByIdQuery"/>.
/// </summary>
public sealed class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Book identifier must be greater than 0.");
    }
}
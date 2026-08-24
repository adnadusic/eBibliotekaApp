namespace Market.Application.Modules.Catalog.Books.Commands.Create;

/// <summary>
/// FluentValidation validator for <see cref="CreateBookCommand"/>.
/// </summary>
public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.");

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .WithMessage("ISBN is required.")
            .MaximumLength(20)
            .WithMessage("ISBN can be up to 20 characters long.");

        RuleFor(x => x.LanguageId)
            .GreaterThan(0)
            .WithMessage("Language is required.");

        RuleFor(x => x.PublicationYear)
            .GreaterThan(0)
            .WithMessage("Publication year must be greater than 0.")
            .When(x => x.PublicationYear.HasValue);

        RuleFor(x => x.PageCount)
            .GreaterThan(0)
            .WithMessage("Page count must be greater than 0.")
            .When(x => x.PageCount.HasValue);

        RuleFor(x => x.PublisherId)
            .GreaterThan(0)
            .WithMessage("Publisher identifier must be greater than 0.")
            .When(x => x.PublisherId.HasValue);

        RuleForEach(x => x.AuthorIds)
            .GreaterThan(0)
            .WithMessage("Author identifier must be greater than 0.");

        RuleForEach(x => x.GenreIds)
            .GreaterThan(0)
            .WithMessage("Genre identifier must be greater than 0.");
    }
}
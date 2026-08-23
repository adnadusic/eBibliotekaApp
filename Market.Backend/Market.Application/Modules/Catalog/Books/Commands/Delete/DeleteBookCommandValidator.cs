namespace Market.Application.Modules.Catalog.Books.Commands.Delete;

/// <summary>
/// FluentValidation validator for <see cref="DeleteBookCommand"/>.
/// </summary>
public sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Book identifier must be greater than 0.");
    }
}
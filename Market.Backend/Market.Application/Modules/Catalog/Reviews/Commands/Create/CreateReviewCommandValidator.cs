namespace Market.Application.Modules.Catalog.Reviews.Commands.Create;

/// <summary>
/// FluentValidation validator for <see cref="CreateReviewCommand"/>.
/// </summary>
public sealed class CreateReviewCommandValidator
    : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0)
            .WithMessage("Book identifier must be greater than 0.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Review title is required.")
            .MaximumLength(200)
            .WithMessage("Review title can be up to 200 characters long.");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("Review comment is required.")
            .MaximumLength(2000)
            .WithMessage("Review comment can be up to 2000 characters long.");
    }
}
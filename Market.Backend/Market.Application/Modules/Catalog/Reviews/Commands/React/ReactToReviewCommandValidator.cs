namespace Market.Application.Modules.Catalog.Reviews.Commands.React;

public sealed class ReactToReviewCommandValidator : AbstractValidator<ReactToReviewCommand>
{
    public ReactToReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .GreaterThan(0).WithMessage("Review ID must be greater than 0.");

        RuleFor(x => x.ReactionType)
            .IsInEnum().WithMessage("Reaction type is invalid.");
    }
}

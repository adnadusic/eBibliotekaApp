using Market.Application.Modules.Catalog.Reviews.Commands.Create;

namespace Market.Tests;

public class CreateReviewCommandValidatorTests
{
    [Fact]
    public void Validate_ShouldPass_WhenReviewDataIsValid()
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();

        var command = new CreateReviewCommand
        {
            BookId = 1,
            Rating = 5,
            Title = "Excellent book",
            Comment = "Very interesting and well written."
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenRatingIsOutsideAllowedRange()
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();

        var command = new CreateReviewCommand
        {
            BookId = 1,
            Rating = 6,
            Title = "Review title",
            Comment = "Review comment"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateReviewCommand.Rating));
    }
}
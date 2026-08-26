using Market.Application.Modules.Catalog.Reviews.Commands.Create;
using Market.Application.Modules.Catalog.Reviews.Commands.React;
using Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController(IMediator mediator) : ControllerBase
{
    [HttpGet("book/{bookId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<GetReviewsByBookItemDto>>> GetByBook(
        int bookId,
        CancellationToken ct)
    {
        var result = await mediator.Send(
            new GetReviewsByBookQuery
            {
                BookId = bookId
            },
            ct);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateReviewCommandDto>> Create(
        [FromBody] CreateReviewCommand command,
        CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);

        return Ok(result);
    }

    [HttpPost("react")]
    [Authorize]
    public async Task<IActionResult> React(
        [FromBody] ReactToReviewCommand command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);

        return NoContent();
    }
}
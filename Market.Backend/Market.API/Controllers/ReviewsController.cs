using Market.Application.Common;
using Market.Application.Modules.Catalog.Reviews.Commands.Create;
using Market.Application.Modules.Catalog.Reviews.Commands.React;
using Market.Application.Modules.Catalog.Reviews.Queries.GetByBook;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController(IMediator mediator) : ControllerBase
{
    [HttpGet("book/{bookId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<PageResult<GetReviewsByBookItemDto>>> GetByBook(
        int bookId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(
            new GetReviewsByBookQuery
            {
                BookId = bookId,
                Paging = new PageRequest
                {
                    Page = Math.Max(1, page),
                    PageSize = pageSize
                }
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
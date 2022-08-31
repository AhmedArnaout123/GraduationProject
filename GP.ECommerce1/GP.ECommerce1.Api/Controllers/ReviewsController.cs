using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Sql.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewsController
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result> AddReview([FromBody] CreateReviewCommand command)
    {
        return await _mediator.Send(command);
    }
}
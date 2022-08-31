using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiscountsController
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result> AddDiscount([FromBody] CreateDiscountCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<Discount>>> GetDiscounts([FromQuery] GetDiscountsQuery query)
    {
        return await _mediator.Send(query);
    }
}
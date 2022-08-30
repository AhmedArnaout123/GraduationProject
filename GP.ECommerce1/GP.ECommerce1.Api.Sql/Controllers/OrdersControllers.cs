using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Application.Orders.Queries.GetOrders;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Sql.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result> AddOrder([FromBody] CreateOrderCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<Order>>> GetOrders([FromQuery] GetOrdersQuery query)
    {
        return await _mediator.Send(query);
    }
}
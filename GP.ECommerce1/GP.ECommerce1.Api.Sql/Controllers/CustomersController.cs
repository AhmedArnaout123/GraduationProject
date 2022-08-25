using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;
using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;
using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Sql.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result> AddCustomer([FromBody] CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPost("AddAddress")]
    public async Task<Result> AddAddress([FromBody] AddAddressCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPost("AddProductToWishList")]
    public async Task<Result> AddProductToWishList([FromBody] AddProductToWishListCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPost("AddProductToShoppingCart")]
    public async Task<Result> AddProductToShoppingCart([FromBody] AddProductToShoppingCartCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<Customer>>> GetCustomers([FromQuery] GetCustomersQuery query)
    {
        return await _mediator.Send(query);
    }
}
﻿using GP.ECommerce1.Core.Application.Testing;
using GP.ECommerce1.Core.Application.Testing.Orders;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DbTestingController
{
    private readonly IMediator _mediator;

    public DbTestingController(IMediator mediator)
    {
        _mediator = mediator;
    }   
    
    [HttpGet("GetCategoryProducts")]
    public async Task<TestingResult> GetCategoryProducts([FromQuery] GetCategoryProductsTestingQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpGet("GetProduct")]
    public async Task<TestingResult> GetProduct([FromQuery] GetProductTestingQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpGet("AwaitingConfirmationOrders")]
    public async Task<TestingResult> GetAwaitingConfirmationOrders([FromQuery] GetAwaitingConfirmationOrdersTestingQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }
}
using GP.ECommerce1.Core.Application.Testing;
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
    
    [HttpGet]
    public async Task<TestingResult> GetCategoryProducts([FromQuery] GetCategoryProductsTestingQuery query)
    {
        return await _mediator.Send(query);
    }
}
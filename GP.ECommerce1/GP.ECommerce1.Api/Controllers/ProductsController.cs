using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.ECommerce1.Core.Application.Products.GetShoppingCartProducts;
using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.ECommerce1.Core.Application.Products.Queries.GetProduct;
using GP.ECommerce1.Core.Application.Products.Queries.GetProducts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result> AddProduct([FromBody] CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<Product>>> GetProducts([FromQuery] GetProductsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("CategoryProducts")]
    public async Task<Result<GetCategoryProductsQueryResponse>> GetCategoryProducts(
        [FromQuery] GetCategoryProductsQuery query)
    {
        return await _mediator.Send(query);
    }
    
    [HttpGet("{id}")]
    public async Task<Result<GetProductQueryResponse>> GetProduct(Guid id)
    {
        return await _mediator.Send(new GetProductQuery{ProductId = id});
    }
    
    [HttpGet("ShoppingCartProducts")]
    public async Task<Result<ShoppingCart>> GetShoppingCartProducts([FromQuery] GetShoppingCartProductsQuery query)
    {
        return await _mediator.Send(query);
    }
}
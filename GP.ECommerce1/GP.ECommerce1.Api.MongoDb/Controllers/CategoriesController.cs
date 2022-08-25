using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GP.ECommerce1.Api.MongoDb.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result> AddCategory([FromBody] CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<Result<List<Category>>> GetCategories([FromQuery] GetCategoriesQuery query)
    {
        return await _mediator.Send(query);
    }
}
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Application.Products.Queries.GetProducts;
using GP.ECommerce1.Core.Domain;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder;

public class DataSeedingHelper
{
    private readonly IMediator _mediator;

    public DataSeedingHelper(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<List<Category>> GetAllCategories()
    {
        var query = new GetCategoriesQuery();
        var result = await _mediator.Send(query);
        return result.Value;
    }

    public async Task<List<Discount>> GetAllDiscounts()
    {
        var query = new GetDiscountsQuery();
        var result = await _mediator.Send(query);
        return result.Value;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return (await _mediator.Send(new GetCustomersQuery())).Value;
    }
    
    public async Task<List<Product>> GetAllProducts()
    {
        return (await _mediator.Send(new GetProductsQuery())).Value;
    }
}
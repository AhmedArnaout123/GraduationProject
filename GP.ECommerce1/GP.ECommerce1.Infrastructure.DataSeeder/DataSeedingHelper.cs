using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;
using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Application.Products.Queries.GetProducts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder;

public class DataSeedingHelper
{
    private readonly IMediator _mediator;

    private List<CreateCategoryCommand> CategoriesCommands { get; set; } = new();
    
    private List<CreateDiscountCommand> DiscountCommands { get; set; } = new();

    public DataSeedingHelper(IMediator mediator)
    {
        _mediator = mediator;
    }
}
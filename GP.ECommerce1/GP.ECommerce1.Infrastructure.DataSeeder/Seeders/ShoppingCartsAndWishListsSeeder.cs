using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;
using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class ShoppingCartsAndWishListsSeeder
{
    private readonly IMediator _mediator;
    private readonly DataSeedingHelper _dataSeedingHelper;

    public ShoppingCartsAndWishListsSeeder(IMediator mediator, DataSeedingHelper dataSeedingHelper)
    {
        _mediator = mediator;
        _dataSeedingHelper = dataSeedingHelper;
    }

    public async Task Seed()
    {
        var customers = await _dataSeedingHelper.GetAllCustomers();
        var products = await _dataSeedingHelper.GetAllProducts();
        foreach (var customer in customers)
        {
            for (int i = 0; i < Randoms.RandomInt(4); i++)
            {
                var index = Randoms.RandomInt(products.Count);
                var command = new AddProductToShoppingCartCommand
                {
                    Quantity = Randoms.RandomQuantity(),
                    CustomerId = customer.Id,
                    ProductId = products[index].Id
                };
                await _mediator.Send(command);
            }
            for (int i = 0; i < Randoms.RandomInt(4); i++)
            {
                var index = Randoms.RandomInt(products.Count);
                var command = new AddProductToWishListCommand
                {
                    CustomerId = customer.Id,
                    ProductId = products[index].Id
                };
                await _mediator.Send(command);
            }
        }
    }
}
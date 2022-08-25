﻿using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder;

public class DataSeedingManager
{
    
// using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
// {
//     var mediator = serviceProvider.GetRequiredService<IMediator>();
//     var seedingManager = new DataSeedingManager(mediator);
//     await seedingManager.SeedCategories();
//     await seedingManager.SeedDiscounts();
// }
    private readonly IMediator _mediator;

    private readonly DataSeedingHelper _dataSeedingHelper;

    public DataSeedingManager(IMediator mediator)
    {
        _mediator = mediator;
        _dataSeedingHelper = new DataSeedingHelper(mediator);
    }
    
    public static void SeedData(){}

    public async Task SeedCategories()
    {
        var seeder = new CategoriesSeeder(_mediator);
        await seeder.SeedSql();
    }
    
    public async Task SeedDiscounts()
    {
        var seeder = new DiscountsSeeder(_mediator);
        await seeder.Seed();
    }

    public async Task SeedProducts()
    {
        var seeder = new ProductsSeeder(_mediator, _dataSeedingHelper);
        await seeder.Seed();
    }

    public async Task SeedCustomers()
    {
        var seeder = new CustomersSeeder(_mediator);
        await seeder.Seed();
    }
    
    public async Task SeedReviews()
    {
        var seeder = new ReviewsSeeder(_mediator, _dataSeedingHelper);
        await seeder.Seed();
    }

    public async Task SeedAddresses()
    {
        var seeder = new AddressesSeeder(_mediator, _dataSeedingHelper);
        await seeder.Seed();
    }

    public async Task SeedShoppingCartsAndWishLists()
    {
        var seeder = new ShoppingCartsAndWishListsSeeder(_mediator, _dataSeedingHelper);
        await seeder.Seed();
    }

    public async Task SeedOrders(int count)
    {
        var seeder = new OrdersSeeder(_mediator, _dataSeedingHelper);
        await seeder.Seed(count);
    }
}
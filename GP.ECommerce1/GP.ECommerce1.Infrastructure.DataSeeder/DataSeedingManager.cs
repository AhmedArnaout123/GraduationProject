using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
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

    private CategoriesSeeder CategoriesSeeder { get; set; }
    private DiscountsSeeder DiscountsSeeder { get; set; }
    private OrdersSeeder OrdersSeeder { get; set; }
    private ProductsSeeder ProductsSeeder { get; set; }
    private CustomersSeeder CustomersSeeder { get; set; }
    private ReviewsSeeder ReviewsSeeder { get; set; }
    
    public static string CategoriesFileName => "Categories";
    public static string DiscountsFileName => "Discounts";

    #region Load/1000

    public static string Products1000FileName => "Products1000";
    public static string Customers1000FileName => "Customers1000";
    public static string Orders1000FileName => "Orders1000";
    public static string Reviews1000FileName => "Reviews1000";

    #endregion
    
    #region Load/250000

    public static string Products250000FileName => "Products250000";
    public static string Customers250000FileName => "Customers250000";
    public static string Orders250000FileName => "Orders250000";
    public static string Reviews250000FileName => "Reviews250000";

    #endregion

    public DataSeedingManager(IMediator mediator)
    {
        CategoriesSeeder = new(mediator);
        DiscountsSeeder = new(mediator);
        OrdersSeeder = new(mediator);
        ProductsSeeder = new(mediator);
        CustomersSeeder = new(mediator);
        ReviewsSeeder = new(mediator);
    }

    public async Task CreateMasterData()
    {
        await Task.Run(() => CategoriesSeeder.GenerateAndStoreAsJson());
        await Task.Run(() => DiscountsSeeder.GenerateAndStoreAsJson());
    }

    public async Task SeedMasterData()
    {
        await CategoriesSeeder.Seed(CategoriesFileName);
        await DiscountsSeeder.Seed(DiscountsFileName);
    }

    public async Task Create1000()
    {
        await Task.Run(() => ProductsSeeder.GenerateAndStoreAsJson(Products1000FileName, CategoriesFileName, DiscountsFileName, 1000));
        await Task.Run(() => CustomersSeeder.GenerateAndStoreAsJson(Customers1000FileName, 1000));
        await Task.Run(() => ReviewsSeeder.GenerateAndStoreAsJson(Reviews1000FileName, Customers1000FileName, Products1000FileName, 1000));
        await Task.Run(() => OrdersSeeder.GenerateAndStoreAsJson(Orders1000FileName, Customers1000FileName, Products1000FileName, 1000));
    }

    public async Task Seed1000()
    {
        await ProductsSeeder.Seed(Products1000FileName);
        await CustomersSeeder.Seed(Customers1000FileName);
        await ReviewsSeeder.Seed(Reviews1000FileName);
        await OrdersSeeder.Seed(Orders1000FileName);
    }
    
    public async Task Create250000()
    {
        await Task.Run(() => ProductsSeeder.GenerateAndStoreAsJson(Products250000FileName, CategoriesFileName, DiscountsFileName, 250000));
        await Task.Run(() => CustomersSeeder.GenerateAndStoreAsJson(Customers250000FileName, 250000));
        await Task.Run(() => ReviewsSeeder.GenerateAndStoreAsJson(Reviews250000FileName, Customers250000FileName, Products250000FileName, 250000));
        await Task.Run(() => OrdersSeeder.GenerateAndStoreAsJson(Orders250000FileName, Customers250000FileName, Products250000FileName, 250000));
    }

    public async Task Seed250000()
    {
        await ProductsSeeder.Seed(Products250000FileName);
        await CustomersSeeder.Seed(Customers250000FileName);
        await ReviewsSeeder.Seed(Reviews250000FileName);
        await OrdersSeeder.Seed(Orders250000FileName);
    }
}
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class ProductsSeeder
{
    private readonly IMediator _mediator;
    
    private static List<Product> Products { get; set; } = new();

    public ProductsSeeder(IMediator mediator)
    {
        _mediator = mediator;
        GenerateInitialData();
    }

    public async Task Seed(int count)
    {
        Console.WriteLine("Seeding Products....");
        var categoriesCommands = await Task.Run(CategoriesSeeder.GetAllCategories);
        var discountsCommands =  await Task.Run(DiscountsSeeder.GetAllDiscounts);

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 1; i <= count; i++)
        {
            var imagesUri = new List<string>();
            for (int j = 1; j <= Randoms.RandomInt(0, 8); j++)
            {
                imagesUri.Add(_defaultImageUrl);
            }

            var childCategories = categoriesCommands.Where(c => c.ParentId != null).ToList();
            var category = childCategories[Randoms.RandomInt(childCategories.Count)];
            var discountCommand = discountsCommands[Randoms.RandomInt(discountsCommands.Count)];
            var discount = new Discount()
            {
                Description = discountCommand.Description,
                Id = discountCommand.Id,
                Percentage = discountCommand.Percentage
            };
            var command = new CreateProductCommand
            {
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                CategoryName = category.Name,
                Discount = Randoms.RandomBoolean() ? discount : null,
                Description = _productsDescriptions[Randoms.RandomInt(_productsDescriptions.Length)],
                Name = _productsNames[Randoms.RandomInt(_productsNames.Length)],
                Price = Randoms.RandomPrice(),
                MainImageUri = _defaultImageUrl,
                Images = imagesUri
            };
            await _mediator.Send(command);
            Console.WriteLine(i);
        }

        stopWatch.Stop();
        Console.WriteLine("Seeding Products Succeeded");
        Console.WriteLine($"Time Consumed: {stopWatch}");
    }

    public static void GenerateAndStoreAsJson(int count)
    {
        Console.WriteLine("Generating Products....");
        var categories =  Task.Run(CategoriesSeeder.GetAllCategories).Result;
        var discounts =   Task.Run(DiscountsSeeder.GetAllDiscounts).Result;
        List<Product> products = new();
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 1; i <= count; i++)
        {
            var imagesUri = new List<string>();
            for (int j = 1; j <= Randoms.RandomInt(0, 8); j++)
            {
                imagesUri.Add(_defaultImageUrl);
            }

            var childCategories = categories.Where(c => c.ParentId != null).ToList();
            var category = childCategories[Randoms.RandomInt(childCategories.Count)];
            var discount = discounts[Randoms.RandomInt(discounts.Count)];
            
            var product = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                CategoryName = category.Name,
                Discount = Randoms.RandomBoolean() ? discount : null,
                Description = _productsDescriptions[Randoms.RandomInt(_productsDescriptions.Length)],
                Name = _productsNames[Randoms.RandomInt(_productsNames.Length)],
                Price = Randoms.RandomPrice(),
                MainImageUri = _defaultImageUrl,
                Images = imagesUri
            };
            products.Add(product);
            Console.WriteLine(i);
        }

        Task.Run(() => FilesHelper.WriteToJsonFile("Products", products)).Wait();
        stopWatch.Stop();
        Products = products;
        Console.WriteLine("Generating Products Succeeded");
        Console.WriteLine($"Time Consumed: {stopWatch.Elapsed}");
    }
    
    public static List<Product> GetAllProducts()
    {
        if (Products.Any())
            return Products;
        var products = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Product>>("Products")).Result;
        Products = products;
        return products;
    }

    private static string[] _productsNames = new String[100];
    private static string[] _productsDescriptions = new string[100];
    private static string _defaultImageUrl = "https://www.themelocation.com/wp-content/uploads/2015/01/woocommerce113.jpg";

    private static void GenerateInitialData()
    {
        for (int i = 0; i < _productsNames.Length; i++)
        {
            _productsNames[i] = Randoms.RandomSentence(Randoms.RandomInt(2, 10));
        }

        for (int i = 0; i < _productsDescriptions.Length; i++)
        {
            _productsDescriptions[i] = Randoms.RandomDescription();
        }
    }
}